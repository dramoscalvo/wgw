using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    private Rigidbody _rigidBody;

    public float impulseForce;
    private Vector3 tapPosition;
    private Vector3 mousePosition;
    private static bool isPressed;
    private GameObject[] arms;
    private float xMovementMin;
    private void Start() 
    {
        _rigidBody = GetComponent<Rigidbody>();
        arms = GameObject.FindGameObjectsWithTag("Arm");
        xMovementMin = 0.1f;
    }

    private void Update() {
        tapPosition = mousePosition;
        mousePosition = Input.mousePosition;
    }


    private void OnMouseDown() {
        ApplyImpulseToGameObject(Vector3.left, false);
        isPressed = true;
    }
    
    private void OnMouseUp() {
        isPressed = false;
        EnableColliders(arms);
    }

    private void OnMouseOver() {
        float xMovement;
        Vector3 localRightDirectionNormalized = _rigidBody.transform.right.normalized;
        if(isPressed){
            xMovement = tapPosition.x - mousePosition.x;
            if(xMovement > xMovementMin){
                ApplyImpulseToGameObject(localRightDirectionNormalized, true);
            }else if(xMovement < -xMovementMin){
                ApplyImpulseToGameObject(localRightDirectionNormalized.normalized, false);
            }
        }
    }

    /// <summary>
    /// Enables the collider of all the elements in a collection
    /// </summary>
    /// <param name="objects">Vector of GameObjects to which activate collider to</param>
    private void EnableColliders(GameObject[] gameObjects){
        foreach(GameObject element in gameObjects){
            element.GetComponent<Collider>().enabled = true;
        }
    }

    /// <summary>
    /// Disable the collider of current GameObject
    /// </summary>
    private void DisableArmCollider(){
        _rigidBody.GetComponent<Collider>().enabled = false;
    }

    /// <summary>
    /// Apply the logic of giving an impulse to a GameObject
    /// </summary>
    /// <param name="forceDirection">Direction to which apply the force</param>
    /// <param name="revertForce">True if the force direction has to be reverted</param>
    private void ApplyImpulseToGameObject (Vector3 forceDirection, bool revertForce ){

        if(revertForce){
            forceDirection *= -1;
        }

        _rigidBody.AddForce(forceDirection * impulseForce, ForceMode.Impulse);
        tapPosition = Input.mousePosition;
        EnableColliders(arms);
        DisableArmCollider();
    }

}
