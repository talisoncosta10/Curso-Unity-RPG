using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
   private bool isPaused = false;
   public GameObject img;

    // Update is called once per frame
    void Update()
    {
        returnMenu();
        callPauseResume();
    }

    public void returnMenu()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void callPauseResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        img.SetActive(true);   
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        img.SetActive(false);
    }

}
