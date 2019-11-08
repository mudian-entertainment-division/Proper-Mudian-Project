using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject _pauseMenu;
    public GameObject _settingsMenu;
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    public void OpenSettings()
    {
        if(Setting.SettingsOpen)
        {
            Setting.SettingsOpen = false;
            _settingsMenu.SetActive(false);
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Setting.SettingsOpen = true;
            _settingsMenu.SetActive(true);
            _pauseMenu.SetActive(false);
            Time.timeScale = 0;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; // lock the mouse cursor
            Cursor.visible = false;
            isPaused = false;
            return;

        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None; // lock the mouse cursor
            Cursor.visible = true;
            _pauseMenu.SetActive(true);
            isPaused = true;
            return;
        }
    }
}
