using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckFlip : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isFlipComplete = false;
    private int _count;
    private float _angle;


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
        CountFlips();
        
        Debug.Log("Count = " + _count);
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
        if (!_groundChecker.IsTrackedLayer())
            _count += 1;
    }

    private void CountFlips()
    {
        bool isDownRayReached = false;
        bool isUpRayReached = false;
        
        if (!_groundChecker.IsTrackedLayer())
        {
            isUpRayReached = IsRayFromPlayerReachedPlane(Vector3.up);
            isDownRayReached = IsRayFromPlayerReachedPlane(Vector3.down);
        }

        if (!isUpRayReached && isDownRayReached && _isFlipComplete)
            _isFlipComplete = false;

        if (isUpRayReached && !isDownRayReached && !_isFlipComplete)
        {
            _count++;
            _isFlipComplete = true;
        }
    }

    private bool IsRayFromPlayerReachedPlane(Vector3 direction)
    {
        RaycastHit hit;
        bool isRayReached = false;
            
        var playerPosition = _player.transform.position;
        var playerDirection = _player.transform.TransformDirection(direction);

        int ignoreLayer = 1 << _player.gameObject.layer;
        ignoreLayer = ~ignoreLayer;

        if (Physics.Raycast(playerPosition, playerDirection, out hit, 150f, ignoreLayer))
        {
            int hitLayer = 1 << hit.collider.gameObject.layer;
            float distantionPlayerPlaneZ =
                DistantionAlongZAxis(_player.transform.position.z, hit.collider.gameObject.transform.position.z);
            
            if (hitLayer == _groundLayer && distantionPlayerPlaneZ < 3f)
            {
                Debug.Log("Ray up " + hit.collider.gameObject.tag);
                isRayReached = true;
            }
        }

        return isRayReached;
    }

    private float DistantionAlongZAxis(float from, float to)
    {
        return Mathf.Abs(from- to);
    }
}
