using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerDeaths = 0;
    [SerializeField] Text deathText;
    [SerializeField] Text timerText;
    [SerializeField] float startTime;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        } 
     }

    // Use this for initialization
    void Start () 
    {
        startTime = Time.time;
		// deathText.text = "Deaths: " + playerDeaths.ToString();
	}
	
    private void Update()
    {
        float t = Time.time-startTime;
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00.00");
        timerText.text = "Time\n " + minutes + ":" + seconds;
         deathText.text = "Deaths: " + playerDeaths.ToString();
    }

    public void ProcessPlayerDeath()
    {
        // if (playerLives > 1)
        // {
        //     incrementDeaths();
        // }
        // else
        // {
        //     ResetGameSession();
        // }
        if(playerDeaths >= 0){
            incrementDeaths();
        }
    }

    private void incrementDeaths()
    {
        playerDeaths++;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        deathText.text = "Deaths: " + playerDeaths.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
