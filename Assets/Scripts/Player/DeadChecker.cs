using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadChecker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private ResultScore _resultScore;

    private void OnValidate()
    {
        if (_scoreCounter == null)
            _scoreCounter = FindObjectOfType<ScoreCounter>();
        if (_player == null)
            _player = FindObjectOfType<Player>();
        if(_resultScore == null)
            _resultScore = FindObjectOfType<ResultScore>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            _resultScore.Show();
            _player.IsDead = true;
        }
    }
}
