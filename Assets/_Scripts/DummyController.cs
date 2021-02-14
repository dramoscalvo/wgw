using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DummyController : MonoBehaviour
{
    private static bool isPressed;

    private Rigidbody _rigidBody;

    private GameManager gameManager;
    private Vector3 tapPosition;
    private Vector3 mousePosition;
    private GameObject[] targets;
    private float xMovementMin;
    private bool hasSpawneable;
    private int randomValue;
    private int index;

    public List<GameObject> spawneables;
    public float trySpawnTime;
    public float impulseForce;
    public int randomRangeMin;
    public int randomRangeMax;
    public int spawnTriggerValue;
    public float fatigueBase;
    public float fatigueAccum;

    private void Start() 
    {
        _rigidBody = GetComponent<Rigidbody>();
        targets = GameObject.FindGameObjectsWithTag("Arm");
        xMovementMin = 0.1f;
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnCollectable());
    }

    private void Update() {
        tapPosition = mousePosition;
        mousePosition = Input.mousePosition;
        
    }

    private void OnMouseDown() {
        ApplyImpulseToGameObject(Vector3.back, false);
        isPressed = true;
        hasSpawneable = false;
        foreach (GameObject element in spawneables){
            element.SetActive(false);
        }
        gameManager.UpdateFatigue(fatigueBase);
    }
    
    private void OnMouseUp() {
        isPressed = false;
        EnableColliders(targets);
        fatigueAccum = 0;
    }

    private void OnMouseOver() {
        if(isPressed){
            float xMovement;
            Vector3 localRightDirectionNormalized = _rigidBody.transform.right.normalized;
            xMovement = tapPosition.x - mousePosition.x;
            if(xMovement > xMovementMin){
                ApplyImpulseToGameObject(localRightDirectionNormalized, true);
                hasSpawneable = false;
                foreach (GameObject element in spawneables){
                    element.SetActive(false);
                }
                gameManager.UpdateFatigue(fatigueBase + fatigueAccum);
                fatigueAccum++;
            }else if(xMovement < -xMovementMin){
                ApplyImpulseToGameObject(localRightDirectionNormalized, false);
                hasSpawneable = false;
                foreach (GameObject element in spawneables){
                    element.SetActive(false);
                }
                gameManager.UpdateFatigue(fatigueBase + fatigueAccum);
                fatigueAccum++;
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

    /// <summary>
    /// Co-routine to spawn spawneables objects
    /// </summary>
    IEnumerator SpawnCollectable(){
        while (true){
            yield return new WaitForSeconds(trySpawnTime);
            if(!hasSpawneable){
                randomValue = Random.Range(randomRangeMin, randomRangeMax);
                if(randomValue < spawnTriggerValue){
                    index = Random.Range(0, spawneables.Count);
                    spawneables[index].SetActive (true);
                    hasSpawneable = true;
                } 
            }
        }
    }

}
