using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckFlip : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private int _groundLayer = 8;
    private int _count;
    private float _angle;
    private bool _isFlipComplete = true;
    private bool _isFlipStart = false;


    #region MonoBehabiour
    private void OnValidate()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>();
        if (_groundChecker == null)
            _groundChecker = FindObjectOfType<GroundChecker>();
    }

    private void FixedUpdate()
    {
        if (!_groundChecker.IsTrackedLayer())
        {
            LetRayUp();
            LetRayDown();
        }
        Debug.Log(_count);
    }

    #endregion

    public int Count
    {
        get => _count;
    }

    public void Clear()
    {
        _count = 0;
    }
    
    private void CorrectionCount()
    {
        if (_isFlipComplete && !_groundChecker.IsTrackedLayer())
            _count += 1;
    }

    private void LetRayUp()
    {
        RaycastHit hit;
        
        var playerPosition = _player.transform.position;
        var playerDirectionUp = _player.transform.TransformDirection(Vector3.up);
        if (_isFlipStart)
        {
            if (Physics.Raycast(playerPosition, playerDirectionUp, out hit, 150f))
            {
                if (hit.collider.gameObject.layer == _groundLayer)
                {
                    Debug.Log("Ray up " + hit.collider.gameObject.tag);
                    _isFlipComplete = true;
                    _isFlipStart = false;
                    _count++;
                }
            }

            Color rayColor = Color.red;

            if (hit.collider.gameObject.layer == _groundLayer)
                rayColor = Color.green;

            Debug.DrawRay(playerPosition, playerDirectionUp * 100, rayColor, 0.1f, false);
        }
    }
    
    private void LetRayDown()
    {
        RaycastHit hit;
        
        var playerPosition = _player.transform.position;
        var playerDirectionDown = _player.transform.TransformDirection(Vector3.down);
        
        if (_isFlipComplete)
        {
            if (Physics.Raycast(playerPosition, playerDirectionDown, out hit, 150f))
            {
                Color rayColor = Color.red;

                if(hit.collider.gameObject.layer == _groundLayer)
                    rayColor = Color.green;
        
                Debug.DrawRay(playerPosition, playerDirectionDown, rayColor * 100, 0.1f, false);
                
                if (hit.collider.gameObject.layer == _groundLayer)
                {
                    Debug.Log("Ray down " + hit.collider.gameObject.tag);
                    _isFlipComplete = false;
                    _isFlipStart = true;
                }
            }
        }
    }

    private void CalculateAngle()
    {
        if (_groundChecker.IsTrackedLayer())
        {
            _angle = _player.transform.localRotation.eulerAngles.x;

            if (gameObject.transform.localRotation.eulerAngles.y == 180)
                _angle += 90f;

            Debug.Log("Angle = " + _angle);
        }
    }

    private void CheckStartFlip()
    {
        if (_angle > 180)
        {
            _isFlipStart = true;
        }
    }

    private void CheckCompleteFlip()
    {
        if (_angle > 270 && _isFlipStart)
        {
            _isFlipComplete = true;
            _isFlipStart = false;
        }
        Debug.Log("Flip complete = " + _isFlipComplete);
    }
}
