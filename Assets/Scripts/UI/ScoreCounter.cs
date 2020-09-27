using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speedOfPaointsReduction;
    [SerializeField] private CheckFlip _checkFlip;
    [SerializeField] private Text _scoreUI;
    [SerializeField] private Text _timeUI;
    [SerializeField] private Text _highScoreUI;
    [SerializeField] private float _scoreForFlip;
    private bool _isTimerStarted = false;
    private float _highScore = 0f;
    private float _score;
    private float _time;

    #region MonoBehaviour

    private void OnValidate()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>();
        if (_checkFlip == null)
            _checkFlip = FindObjectOfType<CheckFlip>();
        if (_speedOfPaointsReduction < 0)
            _speedOfPaointsReduction = 0;
    }

    private void Start()
    {
        _highScore = GetSavedHightScore();
    }
    
    private void Update()
    {
        if (_player.IsStarted && !_isTimerStarted)
        {
            _isTimerStarted = true;
            StartCoroutine(Timer());
            
        }

        if (_player.IsStarted == true)
        {
            ShowArguments();

            SetHightScore();

            SaveHightScore();
        }
    }

    private void FixedUpdate()
    {
        CalculateScore();
    }

    #endregion

    public float GetScore()
    {
        return _score;
    }

    public bool IsNewRecord()
    {
        if (_score >= GetSavedHightScore())
            return true;
        else
            return false;
    }
    
    public float GetSavedHightScore()
    {
        return  PlayerPrefs.GetFloat("HightScore");
    }
    private void CalculateScore()
    {
        _score += _player.GetComponent<Rigidbody>().velocity.z / 20;
        
         if(_score > _time/_score && _time/_score > 0)
             _score = _score - _speedOfPaointsReduction;
        
        _score += _scoreForFlip * _checkFlip.GetLastFlipsCount();
        _checkFlip.ClearLastFlipCount();

        if (_score < 0)
            _score = 0;
    }
    private void ShowArguments()
    {
        _timeUI.text = ((int) _time).ToString();
        _scoreUI.text = ((int) _score).ToString();
        _highScoreUI.text = ((int) _highScore).ToString();
    }

    private void SetHightScore()
    {
        if(_score > GetSavedHightScore())
            _highScore = _score;
    }

    private void SaveHightScore()
    {
        PlayerPrefs.SetFloat("HightScore", _highScore);
    }

    IEnumerator Timer()
    {
        _isTimerStarted = false;
        _time += Time.deltaTime;
        yield return _time;
    }
}
