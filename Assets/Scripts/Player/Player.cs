using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : Transport
{
    [SerializeField] private string _name;
    private GroundChecker _groundChecker;
    private bool _isDead = false;
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

        if (IsDead)
        {
            rigidbody.isKinematic = true;
        }
    }

    private void FixedUpdate()
    {
        MovementLogic();

    }

    #endregion

    public bool IsStarted
    {
        get => _isStarted;
    }

    public bool IsDead
    {
        set => _isDead = value;
        get => _isDead;
    }

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
}
