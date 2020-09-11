using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private string _tagGround;
    [SerializeField] private SphereCollider _triggerCollider;
    [SerializeField] private float _radiusChecker;
    private Vector3 _objectWithTrackPosition;
    private bool _isTracked = false;

    #region MonoBehaviour
    private void Start()
    {
        _triggerCollider.isTrigger = true;
        _triggerCollider.radius = _radiusChecker;
        
        if (_tagGround == "")
            _tagGround = "Ground";
    }
    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == _tagGround)
            _isTracked = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == _tagGround)
            _isTracked = false;
    }

    public bool IsTrackedLayer()
    {
        return _isTracked;
    }
}
