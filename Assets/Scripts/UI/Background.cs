using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Background : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnValidate()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        var playerPosition = _player.transform.position;
        var objectPositionX = gameObject.transform.position.x;
        
        gameObject.transform.position = new Vector3(objectPositionX, playerPosition.y, playerPosition.z);
    }
}
