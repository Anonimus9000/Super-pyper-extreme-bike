using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Transform _beginPosition;

    [SerializeField] private Transform _endPosition;

    public Transform GetBeginTransform()
    {
        return _beginPosition;
    }
    
    public Transform GetEndTransform()
    {
        return _endPosition;
    }
}
