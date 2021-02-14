using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private float fatigue;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI fatigueText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateScore(int valueToUpdate){
        score += valueToUpdate;
        scoreText.text = score.ToString();
    }

    public void UpdateFatigue(float valueToUpdate){
        fatigue += valueToUpdate;
        fatigueText.text = fatigue.ToString();
    }
}
