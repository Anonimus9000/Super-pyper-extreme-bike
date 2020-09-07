using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private LayerMask _maskObstacles;
    private Vector3 _position;

    private void Start()
    {
        _position = _target.InverseTransformPoint(transform.position);
    }

    private void Update()
    {
        CalculatePosition();
    }

    private void CalculatePosition()
    {
        var currentPosition = _target.TransformPoint(_position);
        transform.position = Vector3.Lerp(transform.position, currentPosition, _speed * Time.deltaTime);
        
        RaycastHit hit;
        var distance = Vector3.Distance(transform.position, _target.position);
        
        if (Physics.Raycast(_target.position, transform.position - _target.position, out hit, 
            distance, _maskObstacles))
        {
            transform.position = hit.point;
            transform.LookAt(_target);
        }
    }

    private void CalculateRotation()
    {
        var preRotation = _target.rotation;
        _target.rotation = Quaternion.Euler(0, preRotation.y, 0);
        
        _target.rotation = preRotation;
        
        
        var currentRotation = Quaternion.LookRotation(_target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, _speed * Time.deltaTime);


    }
}
