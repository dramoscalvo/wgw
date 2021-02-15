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
//TODO Hacer co privada y lectura con getter
    public Coroutine co;
    public float restTime;
    [SerializeField]
     [Range(-1, -10)]
    private float fatigueRestBase;



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

    public void StartRestCR(){
        co = StartCoroutine(Rest());
        Debug.Log("Started");
    }

    public void StopRestCR(){
        StopCoroutine(co);
        Debug.Log("Stopped");
    }

    private IEnumerator Rest(){
        while (true){
            yield return new WaitForSeconds(restTime);
            UpdateFatigue(fatigueRestBase);
        }
    }
}
