using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundPlacer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Ground _groundPrefab;
    [SerializeField] private Ground _firstGround;
    [Space(5)]
    [SerializeField] private float _minAngleOfRotation = 20f;
    [SerializeField] private float _maxAngleOfRotation = 170f;
    [Space(5)]
    [SerializeField] private float _minAcuteAngle = 30f;
    [SerializeField] private float _maxObtuseAngle = 330f;
    [Space(5)]
    [SerializeField] private float _maxAngleComplication = 90f;
    [Space(5)]
    [SerializeField] private int _maxSpawnedGround = 20;
    [SerializeField] private float _distantionToSpawn = 30f;
    [SerializeField] private ScoreCounter _scoreCounter;
    [Tooltip("Value between 0 and 1")]
    [SerializeField] private AnimationCurve _complicationByScore;
    
    
    private List<Ground> _spawnedGround = new List<Ground>();

    #region MonoBehaviour

    private void OnValidate()
    {
        if (_playerTransform == null)
            _playerTransform = FindObjectOfType<Player>().transform;
        if (_firstGround == null)
            _firstGround = FindObjectOfType<Ground>();
        if(_scoreCounter == null)
            _scoreCounter = FindObjectOfType<ScoreCounter>();
    }

    private void Start()
    {

        _spawnedGround.Add(_firstGround);
    }

    private void Update()
    {
        if (_playerTransform.position.z >= _spawnedGround[_spawnedGround.Count - 1].GetEndTransform().position.z - _distantionToSpawn)
        {
            SpawnGround();
            
        }

        if (_spawnedGround.Count > _maxSpawnedGround)
        {
            Destroy(_spawnedGround[0].gameObject);
            _spawnedGround.RemoveAt(0);
        }

    }
    

    #endregion

    private void SpawnGround()
    {
        var newGround = Instantiate(_groundPrefab);
        
        SetNewGroundRotation(newGround);
        SetNewGoundPosition(newGround);
        
        _spawnedGround.Add(newGround);
    }

    private void SetNewGoundPosition(Ground ground)
    {
        var endPosition = _spawnedGround.Last().GetEndTransform().position;
        var beginLocalPosition = ground.GetBeginTransform().localPosition;

        var newPosition = endPosition - beginLocalPosition;

        ground.transform.position = newPosition;
    }

    private void SetNewGroundRotation(Ground ground)
    {
        var randAngle = Random.Range(_minAngleOfRotation, _maxAngleOfRotation);
        var newRotation = Quaternion.Euler(randAngle, 0f, 0f);
        ground.transform.rotation = newRotation;
        
        CurrectionRandomRotation(ground);
    }

    private void CurrectionRandomRotation(Ground ground)
    {
        var PreRotationEuler = _spawnedGround.Last().transform.rotation.eulerAngles;
        var NextRotationEuler = ground.transform.rotation.eulerAngles;

        float preRotation = EulerToSimpleAngleX(PreRotationEuler);
        float nextRotation = EulerToSimpleAngleX(NextRotationEuler);

        nextRotation = ComplicationAngle(preRotation, nextRotation);

        CorrectMinAcuteAngle(ref preRotation, ref nextRotation);
        CorrectMaxObtuseAngle(ref preRotation, ref nextRotation);

        ground.transform.rotation = Quaternion.Euler(nextRotation, 0, 0);
       
    }

    private void CorrectMinAcuteAngle(ref float anglePreGround, ref float angleNextGround)
    {
        var angleBetweenTwoGround = (180 - anglePreGround) + angleNextGround; 

        if (angleBetweenTwoGround < _minAcuteAngle)
        {
            float different = _minAcuteAngle - angleBetweenTwoGround;
            
            anglePreGround -= different / 2;
            angleNextGround += different / 2;
            
            _spawnedGround.Last().transform.rotation = Quaternion.Euler(anglePreGround, 0, 0);
        }
    }
    
    private void CorrectMaxObtuseAngle(ref float anglePreGround, ref float angleNextGround)
    {
        var angleBetweenTwoGround = (180 - anglePreGround) + (180 - angleNextGround); 

        if (angleBetweenTwoGround > _maxObtuseAngle)
        {
            float different = _minAcuteAngle - angleBetweenTwoGround;
            
            anglePreGround += different / 2;
            angleNextGround -= different / 2;
            
            _spawnedGround.Last().transform.rotation = Quaternion.Euler(anglePreGround, 0, 0);
        }
    }

    private float ComplicationAngle(float anglePreGround, float angleNextGround)
    {
        float complicationValue = _complicationByScore.Evaluate(_scoreCounter.GetScore());
        
        if (Mathf.Abs(anglePreGround - angleNextGround) < 90)
            angleNextGround -= complicationValue;
          
        else if (Mathf.Abs(anglePreGround - angleNextGround) > 90)
            angleNextGround += complicationValue;
        

        if (angleNextGround > _maxAngleOfRotation)
            angleNextGround = _maxAngleOfRotation;
            
        if (angleNextGround < _minAngleOfRotation)
            angleNextGround = _minAngleOfRotation;

        return angleNextGround;
    }

    private float EulerToSimpleAngleX(Vector3 eulerAngle)
    {
        float simpleAngle = eulerAngle.x;

        if (eulerAngle.y == 180)
            simpleAngle = 90 + (90 - eulerAngle.x);

        return simpleAngle;
    }
}
