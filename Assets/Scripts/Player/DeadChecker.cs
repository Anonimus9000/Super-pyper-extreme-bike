using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadChecker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _imageDeadUI;

    private void Start()
    {
        _imageDeadUI.fillAmount = 0;
        
        if (_player == null)
            _player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            _imageDeadUI.fillAmount = 1;
            _player.IsDead = true;
        }
    }
}
