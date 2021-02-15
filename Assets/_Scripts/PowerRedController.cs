using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRedController : MonoBehaviour
{

    private GameManager gameManager;

    public int score;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    private void OnDisable() {
        gameManager.UpdateScore(score);
    }
}
