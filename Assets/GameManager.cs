using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver =false;
    public static bool GameIsPaused =false;
    public static GameManager instance;

    public GameObject PauseMenuUI;
    public GameObject GameOverUI;
    public int Score =1;
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        Score =1;
        PauseMenuUI.SetActive(false);
        GameOverUI.SetActive(false);
        //Debug.Log(SimplePlayerController.Score);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                resume();
            }else
            {
                pause();
            }
            if(GameIsOver)
            {
               SceneManager.LoadScene(0); 
            }
        }
        if(Score < 0)
        {
            Gameover();

        }
        
        
    }
   
   public void MinusScore()
    {
        Score--;
    }

    void resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void pause()
    {
        PauseMenuUI.SetActive(true); 
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void Gameover()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0.2f;
        GameIsOver =true;
    }
    
}
