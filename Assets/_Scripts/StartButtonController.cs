using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{

    private Button _button;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartGame);
    }

    void StartGame(){
        gameManager.StartGame();
    }
}
