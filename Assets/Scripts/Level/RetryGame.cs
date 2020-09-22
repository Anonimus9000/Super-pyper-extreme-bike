using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryGame : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LoadScene _loadScene;

    private void OnValidate()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Retry();
    }

    private void Retry()
    {
        if (_player.IsDead && Input.anyKey)
        {
            if(_loadScene != null)
                _loadScene.Load();
        }
    }
}
