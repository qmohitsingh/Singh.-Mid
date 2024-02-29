using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{


    public TextMeshProUGUI aliensCounterText;
    public int aliensCounter = 12;
    public TextMeshProUGUI elapsedTimeText; 
    private float elapsedTime;
    private bool timerRunning = true;


    public GameObject scoreBoardBackground;
    public GameObject gameOverScreen;
    public GameObject mainMenu;

    public GameObject panel; 

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    private IEnumerator UpdateScoreBoardRoutine()
    {
        while (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            elapsedTimeText.text = "Elapsed: "+Mathf.FloorToInt(elapsedTime).ToString();

            aliensCounterText.text = "Aliens: "+Mathf.FloorToInt(aliensCounter).ToString();

            yield return null; // Yield until the next frame
        }

        aliensCounterText.text = "Aliens: 0";
    }


    void EnableCursorLockMode()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor during gameplay
    }

    void DisableCursorLockMode()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock cursor during gameover
    }

    public void GameOver()
    {
        DisableCursorLockMode();

        playerController.SetIsPlayerAlive(false);

        timerRunning = false;

        panel.SetActive(true);
        mainMenu.SetActive(false);
        gameOverScreen.SetActive(true);
        scoreBoardBackground.SetActive(true);
        
    }

    public void PlayGame()
    {
        initializeGameState();
    }

    public void initializeGameState() {

        playerController.SetIsPlayerAlive(true);

        EnableCursorLockMode();

        StartCoroutine(UpdateScoreBoardRoutine());


        mainMenu.SetActive(false);
        panel.SetActive(false);

        scoreBoardBackground.SetActive(true);

        gameOverScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
