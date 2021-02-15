using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int _score;
    private int score
    {
        set
        {
            _score = Mathf.Clamp(value, 0, 99999);
        }
        get
        {
            return _score;
        }
    }
    private float _fatigue;
    private float fatigue
    {
        set
        {
            _fatigue = Mathf.Clamp(value, 0, 100);
        }
        get
        {
            return _fatigue;
        }
    }
    private Coroutine _co;
    [SerializeField]
     [Range(-1, -10)]
    private float fatigueRestBase;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI fatigueText;
    public Coroutine co
    {
        get
        {
            return _co;
        }
    }
    public float restTime;

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
        _co = StartCoroutine(RestCR());
        Debug.Log("Started");
    }

    public void StopRestCR(){
        StopCoroutine(co);
        Debug.Log("Stopped");
    }

    private IEnumerator RestCR(){
        while (true){
            yield return new WaitForSeconds(restTime);
            UpdateFatigue(fatigueRestBase);
        }
    }
}
