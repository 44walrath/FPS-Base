using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Behaviour PlayerMovement;
    public GameObject crosshair;
    public GameObject settingsMenuUI;

   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                crosshair.SetActive(false);
            }
        }

        // Check if the game is paused before processing mouse input
        if (GameIsPaused)
        {
            // Disable mouse input handling
            DisableMouseInput();
            PlayerMovement.enabled =false;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        crosshair.SetActive(true);

        // Enable mouse input handling
        EnableMouseInput();
        PlayerMovement.enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Disable mouse input handling
        DisableMouseInput();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void Settings()
    {
        settingsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    // Function to disable mouse input handling
    void DisableMouseInput()
    {
        // Disable mouse events
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Function to enable mouse input handling
    void EnableMouseInput()
    {
        // Enable mouse events
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
 
}