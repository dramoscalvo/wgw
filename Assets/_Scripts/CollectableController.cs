using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{

    private GameManager gameManager;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    private void OnDisable() {
        Debug.Log("HE MORIDO");
        gameManager.UpdateScore(1);
    }
}
