using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuadas : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    [SerializeField] private GameObject _gameObject1;
    // Start is called before the first frame update
    private void Update()
    {
        Debug.Log(_gameObject.transform.rotation.eulerAngles);
    }
    
}
