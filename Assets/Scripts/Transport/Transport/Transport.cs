using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Transport : MonoBehaviour, ITransport
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float turnSpeed;
    
    protected Rigidbody rigidbody;

    /// <summary>
    /// Use only in fixedUpdate methood
    /// </summary>
    protected virtual void MoveForward()
    {
        rigidbody.AddRelativeForce(Vector3.forward * (moveSpeed * Time.fixedDeltaTime));
    }
    /// <summary>
    /// Use only in fixedUpdate methood
    /// </summary>
    protected  virtual void MoveRotate(float horizontalAxis)
    {
        rigidbody.AddRelativeTorque(new Vector3(horizontalAxis * turnSpeed * Time.fixedDeltaTime,0f, 0f));
    }

    public virtual void Lose()
    {
        Destroy(gameObject);
    }
}
