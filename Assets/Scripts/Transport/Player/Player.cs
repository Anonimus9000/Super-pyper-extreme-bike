using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : Transport
{
    [SerializeField] private string _name;
    private GroundChecker _groundChecker;
    private float _score;
    private float _completedDistance;
    private float _passedSeconds;
    private bool _isStarted = false;

    #region MonoBehaviour

    private void OnValidate()
    {
        if (moveSpeed < 0)
            moveSpeed = 0;
        
        if (turnSpeed < 0)
            turnSpeed = 0;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        _groundChecker = GetComponentInChildren<GroundChecker>();
        
        rigidbody.constraints = RigidbodyConstraints.FreezePositionX 
                                | RigidbodyConstraints.FreezeRotationY 
                                | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (IsActive())
            _isStarted = true;
        
        if (!_isStarted)
            StartCoroutine(GameTimerPerSecond());
    }

    private void FixedUpdate()
    {

        MovementLogic();
        
        DistanceCalculate();
        
        _score = CalculateScore();
    }

    #endregion

    private void MovementLogic()
    {
        if (Input.GetButton("Fire1") && IsOnGround() && _groundChecker.IsTrackedLayer())
        {
            MoveForward();
        }
        
        if (Input.GetAxis("Horizontal") != 0)
        {
            MoveRotate(Input.GetAxis("Horizontal"));
        }
    }

    private float CalculateScore()
    {
        return (_completedDistance / _passedSeconds);
    }

    private void DistanceCalculate()
    {
        _completedDistance += rigidbody.velocity.z * Time.fixedDeltaTime;
        
        if (_completedDistance < 0)
            _completedDistance = 0;
    }
    
    private bool IsActive()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetButtonDown("Fire1"))
            return true;
        else
            return false;
    }

    private bool IsOnGround()
    {
        return _groundChecker.IsTrackedLayer();
    }

    IEnumerator GameTimerPerSecond()
    {
        yield return new WaitForSeconds(1);
        _passedSeconds += 1;
    }
}
