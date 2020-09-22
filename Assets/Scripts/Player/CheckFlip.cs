using UnityEngine;


public class CheckFlip : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private ParticleSystem _effecctCompleteFlip;
    private bool _isFlipComplete = false;
    private int _lastFlipsCount;
    private int _count;
    private int _preCount;
    private float _angle;


    #region MonoBehabiour
    private void OnValidate()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>();
        if (_groundChecker == null)
            _groundChecker = FindObjectOfType<GroundChecker>();
    }

    private void Update()
    {
        SaveLastFlipsCount();
        EffectIfFlipComplete();
    }

    private void FixedUpdate()
    {
        CountFlips();
    }

    #endregion

    public int Count
    {
        get => _count;
    }

    public int GetLastFlipsCount()
    {
        return _lastFlipsCount;
    }

    public void ClearLastFlipCount()
    {
        _lastFlipsCount = 0;
    }

    public void Clear()
    {
        _count = 0;
        _preCount = 0;
    }
    
    private void CorrectionCount()
    {
        if (!_groundChecker.IsTrackedLayer())
            _count += 1;
    }

    private void EffectIfFlipComplete()
    {
        if (_preCount != _count && _groundChecker.IsTrackedLayer())
        {
            _effecctCompleteFlip.gameObject.transform.rotation = _player.transform.rotation;
            _effecctCompleteFlip.gameObject.transform.position = _player.transform.position;
            _preCount = _count;
            _effecctCompleteFlip.Play();
        }
    }

    private void CountFlips()
    {
        bool isDownRayReached = false;
        bool isUpRayReached = false;
        
        if (!_groundChecker.IsTrackedLayer())
        {
            isUpRayReached = IsRayFromPlayerReachedPlane(Vector3.up);
            isDownRayReached = IsRayFromPlayerReachedPlane(Vector3.down);
        }

        if (!isUpRayReached && isDownRayReached && _isFlipComplete)
            _isFlipComplete = false;

        if (isUpRayReached && !isDownRayReached && !_isFlipComplete)
        {
            _count++;
            _isFlipComplete = true;
        }
    }

    private bool IsRayFromPlayerReachedPlane(Vector3 direction)
    {
        RaycastHit hit;
        bool isRayReached = false;
            
        var playerPosition = _player.transform.position;
        var playerDirection = _player.transform.TransformDirection(direction);

        int ignoreLayer = 1 << _player.gameObject.layer;
        ignoreLayer = ~ignoreLayer;

        if (Physics.Raycast(playerPosition, playerDirection, out hit, 150f, ignoreLayer))
        {
            int hitLayer = 1 << hit.collider.gameObject.layer;
            float distantionPlayerPlaneZ =
                DistantionAlongZAxis(_player.transform.position.z, hit.collider.gameObject.transform.position.z);
            
            if (hitLayer == _groundLayer && distantionPlayerPlaneZ < 3f)
            {
                isRayReached = true;
            }
        }

        return isRayReached;
    }

    private void SaveLastFlipsCount()
    {
        if (_preCount != _count && _groundChecker.IsTrackedLayer())
        {
            _lastFlipsCount = _count - _preCount;
        }
    }

    private float DistantionAlongZAxis(float from, float to)
    {
        return Mathf.Abs(from- to);
    }
}
