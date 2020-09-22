using UnityEngine;

public class MobileMove : MonoBehaviour
{
    private float _horizontalAxis;

    public float GetHorizontalAxis()
    {
        return _horizontalAxis;
    }
    public void LeftMove()
    {
        _horizontalAxis = -1;
    }

    public void RightMove()
    {
        _horizontalAxis = 1;
    }

    public void StopMove()
    {
        _horizontalAxis = 0f;
    }
}
