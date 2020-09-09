using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.iOS;
using Random = UnityEngine.Random;

public class GroundPlacer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Ground _groundPrefab;
    [SerializeField] private Ground _firstGround;
    [SerializeField] private float _minAngleOfRotation;
    [SerializeField] private float _maxAngleOfRotation;
    [SerializeField] private float _minAngleDifference;
    
    private List<Ground> _spawnedGround = new List<Ground>();

    private void Start()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>().transform;
        _spawnedGround.Add(_firstGround);
    }

    private void Update()
    {
        if (_player.position.z >= _spawnedGround[_spawnedGround.Count - 1].GetEndTransform().position.z - 30f)
        {
            SpawnGround();
            
        }

        if (_spawnedGround.Count > 20)
        {
            Destroy(_spawnedGround[0].gameObject);
            _spawnedGround.RemoveAt(0);
        }

    }

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
        var preRotation = _spawnedGround.Last().transform.rotation.eulerAngles;
        var nextRotation = ground.transform.rotation.eulerAngles;

        if (preRotation.y == 180)
            preRotation.x = 90 + (90 - preRotation.x);
        
        if (nextRotation.y == 180)
            nextRotation.x = 90 + (90 - nextRotation.x);

        var difRotationX = preRotation.x - nextRotation.x;

        if (difRotationX > _minAngleDifference)
        {
            preRotation.x -= difRotationX / 2;
            nextRotation.x += difRotationX / 2;
            
            _spawnedGround.Last().transform.rotation = Quaternion.Euler(preRotation);
            ground.transform.rotation = Quaternion.Euler(nextRotation);
        }
    }
}
