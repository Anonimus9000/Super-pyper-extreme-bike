using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LayerTracker : MonoBehaviour
{
    [SerializeField] private LayerMask _layerToTrack;
    [SerializeField] private SphereCollider _triggerCollider;
    private Vector3 _objectWithTrackPosition;
    private bool _isTracked = false;

    #region MonoBehaviour
    private void Start()
    {
        _triggerCollider.isTrigger = true;
    }
    //Не определяет слой ground 
    //Найти другой способ, не box collider is trigger
    private void FixedUpdate()
    {
        CheckColliders();
    }
    #endregion
    
    private void CheckColliders()
    {
        Collider[] colliders = Physics.OverlapSphere(_triggerCollider.transform.position, 
            _triggerCollider.radius, _layerToTrack);
    
        if (colliders.Length == 0)
            _isTracked = false;
        else
        {
            foreach (var collider in colliders)
            {
                if (collider.gameObject.layer == _layerToTrack)
                    _isTracked = true;
            }
        }
    }

    public bool IsTrackedLayer()
    {
        return _isTracked;
    }
}
