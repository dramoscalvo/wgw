using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        menu,
        inGame,
        gameOver
    }

    public GameState gameState;

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
    //TODO: max _injure should be a variable which will be increased while user plays
    private float _injure;
    private float injure
    {
        set
        {
            _injure = Mathf.Clamp(value, 0, 100);
        }
        get
        {
            return _injure;
        }
    }
    private Coroutine _co;
    
    [SerializeField]
    [Range(-1, -100)]
    private float injureRestBase;
    //TODO: configure setter/getter for text, controlling the type of data is being sent
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI injureText;
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
        gameState = GameState.menu;   
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
    /// Update injure counter by a given value
    /// </summary>
    /// <param name="valueToUpdate">Value to update injure counter</param>
    public void UpdateInjure(float valueToUpdate){
        injure += valueToUpdate;
        injureText.text = injure.ToString();
    }

    /// <summary>
    /// Start Rest co-routine
    /// </summary>
    public void StartRestCR(){
        _co = StartCoroutine(RestCR());
    }

    /// <summary>
    /// Stop Rest co-routine
    /// </summary>
    public void StopRestCR(){
        StopCoroutine(co);
    }

    /// <summary>
    /// Decrease injure counter every restTime
    /// </summary>
    /// <returns></returns>
    private IEnumerator RestCR(){
        while (true){
            yield return new WaitForSeconds(restTime);
            UpdateInjure(injureRestBase);
        }
    }
}
