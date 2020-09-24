using UnityEngine;

public class MobileMove : MonoBehaviour
{
    private bool _forwardMove;
    
    private float _horizontalAxis;

    public float GetHorizontalAxis()
    {
        return _horizontalAxis;
    }

    public bool IsMoveForward()
    {
        return _forwardMove;
    }

    public void ForwardMove()
    {
        _forwardMove = true;
    }
    public void LeftMove()
    {
        _horizontalAxis = -1;
    }

    public void RightMove()
    {
        _horizontalAxis = 1;
    }

    public void StopLeftAndRightMove()
    {
        _horizontalAxis = 0f;
    }

    public void StopForwardMove()
    {
        _forwardMove = false;
    }
}
