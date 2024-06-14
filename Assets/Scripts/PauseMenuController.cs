using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private Canvas gameStats;
    [SerializeField] private Canvas pause;
    [SerializeField] private Canvas settings;
    private bool isPaused = false;
    private bool isSettings = false;
    private void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        isPaused = true;
        isSettings = false;
        gameStats.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);        
        Time.timeScale = 0.0f;
    }
    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
        isSettings = false;
        gameStats.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Settings()
    {
        settings.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
        isSettings = true;
    }
    public void Exit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (isSettings) Pause();
                else Continue();
            }
            else if (gameStats.isActiveAndEnabled) Pause();
        }
    }
}
