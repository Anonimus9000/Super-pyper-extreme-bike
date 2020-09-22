using UnityEngine;

public class Player : Transport
{
    [SerializeField] private string _name;
    private GroundChecker _groundChecker;
    private MobileMove _mobileMove;
    private bool _isDead = false;
    private bool _isStarted = false;
    private float _horizontalAxis;

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

        _mobileMove = GetComponent<MobileMove>();

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
        
        SetHorizontalAxis();
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }

    #endregion

    public void Pause()
    {
        _isStarted = false;
    }
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
        
        if (_horizontalAxis != 0)
        {
            MoveRotate(_horizontalAxis);
        }
    }

    private bool IsActive()
    {
        if (_horizontalAxis != 0 || Input.GetButtonDown("Fire1"))
            return true;
        else
            return false;
    }

    private bool IsOnGround()
    {
        return _groundChecker.IsTrackedLayer();
    }

    private void SetHorizontalAxis()
    {
        if (_mobileMove.GetHorizontalAxis() != 0)
            _horizontalAxis = _mobileMove.GetHorizontalAxis();
        else 
            _horizontalAxis = Input.GetAxis("Horizontal");
        
        Debug.Log("Mobile axis = " + _mobileMove.GetHorizontalAxis());
        Debug.Log("HorizontalAxis = " + _horizontalAxis);
    }
}
