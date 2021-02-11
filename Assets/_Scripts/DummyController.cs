using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DummyController : MonoBehaviour
{
    private Rigidbody _rigidBody;

    public float impulseForce;
    private Vector3 tapPosition;
    private Vector3 mousePosition;
    private static bool isPressed;
    private GameObject[] targets;
    private float xMovementMin;
    public List<GameObject> collectables;
    private bool isCollectable;
    private int random;
    private int index;
    private void Start() 
    {
        _rigidBody = GetComponent<Rigidbody>();
        targets = GameObject.FindGameObjectsWithTag("Arm");
        xMovementMin = 0.1f;
        StartCoroutine(SpawnCollectable());
    }

    private void Update() {
        tapPosition = mousePosition;
        mousePosition = Input.mousePosition;
        
    }

    private void OnMouseDown() {
        ApplyImpulseToGameObject(Vector3.left, false);
        isPressed = true;
        isCollectable = false;
        foreach (GameObject element in collectables){
            element.SetActive(false);
        }
    }
    
    private void OnMouseUp() {
        isPressed = false;
        EnableColliders(targets);
    }

    private void OnMouseOver() {
        if(isPressed){
            float xMovement;
            Vector3 localRightDirectionNormalized = _rigidBody.transform.right.normalized;
            xMovement = tapPosition.x - mousePosition.x;
            if(xMovement > xMovementMin){
                ApplyImpulseToGameObject(localRightDirectionNormalized, true);
                isCollectable = false;
                foreach (GameObject element in collectables){
                    element.SetActive(false);
                }
            }else if(xMovement < -xMovementMin){
                ApplyImpulseToGameObject(localRightDirectionNormalized, false);
                isCollectable = false;
                foreach (GameObject element in collectables){
                    element.SetActive(false);
                }
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
    private void DisableCollider(){
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
        EnableColliders(targets);
        DisableCollider();
    }

    IEnumerator SpawnCollectable(){
        while (true){
            yield return new WaitForSeconds(2f);
            if(!isCollectable){
                random = Random.Range(0, 10);
                if(random < 5){
                    index = Random.Range(0, collectables.Count);
                    collectables[index].SetActive (true);
                    isCollectable = true;
                } 
            }
        }
    }

}
