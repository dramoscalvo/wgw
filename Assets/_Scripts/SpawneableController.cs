using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawneableController : MonoBehaviour
{

    private GameManager gameManager;
    private Coroutine co;
    private bool autoDestroy;

    public int score;
    public int spawnTime;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    //TODO: If object is autodestroyed, it may decrease score. Include variable to set the amount of score to decarse in case of autoDestroy.
    private void OnDisable() {
        StopCoroutine(co);
        if (!autoDestroy){
            gameManager.UpdateScore(score);
        }
    }

    private void OnEnable() {
        co = StartCoroutine(SpawnInterval());
        autoDestroy = false;
    }

    private IEnumerator SpawnInterval (){
       yield return new WaitForSeconds(spawnTime); 
       autoDestroy = true;
       this.gameObject.SetActive(false);
    }
}
