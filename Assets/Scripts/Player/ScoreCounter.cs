using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CheckFlip _checkFlip;
    [SerializeField] private Text _scoreUI;
    [SerializeField] private Text _timeUI;
    [SerializeField] private float _scoreForFlip;
    private bool _isTimerStarted = false;
    private float _score;
    private float _time;

    #region MonoBehaviour

    private void OnValidate()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>();
        if (_checkFlip == null)
            _checkFlip = FindObjectOfType<CheckFlip>();
    }

    private void Update()
    {
        if (_player.IsStarted && !_isTimerStarted)
        {
            _isTimerStarted = true;
            StartCoroutine(Timer());
        }

        CalculateScore();
        
        ShowArguments();
    }

    #endregion
    
    private void CalculateScore()
    {
        //не хочет нормально делить на время
        _score += _player.GetComponent<Rigidbody>().velocity.z;
        _score = _score / _time;

        _score += _scoreForFlip * _checkFlip.Count;
    }
    private void ShowArguments()
    {
        _timeUI.text = ((int) _time).ToString();
        _scoreUI.text = ((int) _score).ToString();
    }

    IEnumerator Timer()
    {
        _isTimerStarted = false;
        _time += Time.deltaTime;
        yield return _time;
    }
}
