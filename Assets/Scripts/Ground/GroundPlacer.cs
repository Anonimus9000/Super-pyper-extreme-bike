using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundPlacer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Ground _groundPrefab;
    [SerializeField] private Ground _firstGround;
    [SerializeField] private float _minAngleOfRotation;
    [SerializeField] private float _maxAngleOfRotation;
    
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
            //SpawnGround();
        }
        
    }

    private void SpawnGround()
    { 
        Debug.Log("SPAWN");
        var newGround = Instantiate(_groundPrefab);
        
        SetNewGoundPosition(newGround);
        SetNewGroundRotation(newGround);
        
        _spawnedGround.Add(newGround);
    }

    private void SetNewGoundPosition(Ground ground)
    {
        var endPosition = _spawnedGround.Last().GetEndTransform().position;
        var beginLocalPosition = ground.GetBeginTransform().localPosition;

        var newPosition = endPosition - beginLocalPosition;
                          ;

        ground.transform.position = newPosition;
    }

    private void SetNewGroundRotation(Ground ground)
    {
        //Сделать максимальную разницу в градусах между последним и следующим плентом - 65
        var xRotation = Random.Range(_minAngleOfRotation, _maxAngleOfRotation);
        var yRotation = ground.transform.rotation.y;
        var zRotation = ground.transform.rotation.z;
        
        var newRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

        ground.transform.rotation = newRotation;
    }
}
