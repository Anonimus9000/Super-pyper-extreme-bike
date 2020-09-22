using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    [SerializeField] private Text _resultScore;
    [SerializeField] private Text _recordResultScore;
    [SerializeField] private Image _recordResultImage;
    [SerializeField] private Image _resultImage;
    [SerializeField] private ScoreCounter _scoreCounter;
    private float _highScore;
    private float _score;

    private void OnValidate()
    {
        if(_scoreCounter == null)
            _scoreCounter = FindObjectOfType<ScoreCounter>();
    }

    void Start()
    {
        _resultImage.fillAmount = 0f;
        _recordResultImage.fillAmount = 0f;
        _highScore = _scoreCounter.GetSavedHightScore();
    }
    
    void Update()
    {
        _score = _scoreCounter.GetScore();
    }

    public void Show()
    {
        if (_score > _highScore)
        {
            _recordResultImage.fillAmount = 1f;
            _resultScore.text = ((int)_score).ToString();
            _recordResultScore.text = ((int)_score).ToString();
        }
        else if (_score <= _highScore)
        {
            _resultImage.fillAmount = 1f;
            _resultScore.text = ((int)_score).ToString();
            _recordResultScore.text = ((int)_highScore).ToString();
        }
    }
}
