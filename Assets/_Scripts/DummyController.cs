using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    public float constForce;
    private Rigidbody _rigidBody;
    private void Start() 
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate() {
        if(Input.GetKey("space"))
        {
            _rigidBody.AddTorque(Vector3.up * constForce);
        }
    }

    
}
