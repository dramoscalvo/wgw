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
    //TODO: max _fatigue should be a variable which will be increased while user plays
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
    [Range(-1, -100)]
    private float fatigueRestBase;
    //TODO: configure setter/getter for text, controlling the type of data is being sent
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

    /// <summary>
    /// Update the score by a given value
    /// </summary>
    /// <param name="valueToUpdate">Value to update the score</param>
    public void UpdateScore(int valueToUpdate){
        score += valueToUpdate;
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Update fatigue counter by a given value
    /// </summary>
    /// <param name="valueToUpdate">Value to update fatigue counter</param>
    public void UpdateFatigue(float valueToUpdate){
        fatigue += valueToUpdate;
        fatigueText.text = fatigue.ToString();
    }

    /// <summary>
    /// Start Rest co-routine
    /// </summary>
    public void StartRestCR(){
        _co = StartCoroutine(RestCR());
        Debug.Log("Started");
    }

    /// <summary>
    /// Stop Rest co-routine
    /// </summary>
    public void StopRestCR(){
        StopCoroutine(co);
        Debug.Log("Stopped");
    }

    /// <summary>
    /// Decrease fatigue counter every restTime
    /// </summary>
    /// <returns></returns>
    private IEnumerator RestCR(){
        while (true){
            yield return new WaitForSeconds(restTime);
            UpdateFatigue(fatigueRestBase);
        }
    }
}
