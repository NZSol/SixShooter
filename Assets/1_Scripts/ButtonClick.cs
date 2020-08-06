using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonClick : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClick;
    public GameObject optionsPanel;
    public GameObject pauseMenu;
    public AudioMixer mixer;
    public AudioMixer gameMixer;
    public static bool isPaused;
    bool pauseUI;
    CursorLockMode desiredMode;
    // Start is called before the first frame update
    void Start()
    {
        pauseUI = false;
        optionsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;
        string sceneName = currentScene.name;
        if(pauseMenu == true)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
        if (buildIndex == 2 && Input.GetKeyDown(KeyCode.Escape))
        {
            TimeManager.GamePause = !TimeManager.GamePause;
            pauseMenu.SetActive(false);
            Cursor.visible = false;
            //Debug.Log("Work");
            //TimeManager.endSlow = false;
            //Time.timeScale = 0;
            pauseUI = !pauseUI;
            isPaused = !isPaused;
            PauseUI();
        }
    }
    public void playClip()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
    }
    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    public void SetInGameLevel(float sliderValue)
    {
        gameMixer.SetFloat("InGameSound", Mathf.Log10(sliderValue) * 20);
    }
    public void OpenOptions(bool newValue)
    {
        optionsPanel.SetActive(newValue);

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            QualitySettings.SetQualityLevel(0, true);
        }
        if(val == 1)
        {
            QualitySettings.SetQualityLevel(1, true);
        }
        if (val == 2)
        {
            QualitySettings.SetQualityLevel(2, true);
        }
        if (val == 3)
        {
            QualitySettings.SetQualityLevel(3, true);
        }
        if (val == 4)
        {
            QualitySettings.SetQualityLevel(4, true);
        }
        if (val == 5)
        {
            QualitySettings.SetQualityLevel(5, true);
        }
    }

    public void ResHandleInputData(int val)
    {
        if(val == 0)
        {
            Screen.SetResolution(1280, 720, true);
        }
        if (val == 1)
        {
            Screen.SetResolution(1920, 1200, true);
        }
        if (val == 2)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        if (val == 3)
        {
            Screen.SetResolution(2560, 1440, true);
        }
    }
    public void PauseUI()
    {
        if(pauseUI == true)
        {
            pauseMenu.SetActive(true);
            desiredMode = CursorLockMode.None;
            {
                Cursor.lockState = desiredMode;
            }
            //make time pause
        }
        if (pauseUI == false)
        {
            desiredMode = CursorLockMode.Confined;
            pauseMenu.SetActive(false);

        }
    }
    public void ResumeButton()
    {
        desiredMode = CursorLockMode.Confined;
        isPaused = !isPaused;
        TimeManager.GamePause = !TimeManager.GamePause;
        pauseMenu.SetActive(false);
        pauseUI = !pauseUI;
        PauseUI();
    }

}
