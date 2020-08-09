using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using DigitalRuby.SimpleLUT;
using UnityEngine.Rendering.PostProcessing;

public class ButtonClick : MonoBehaviour
{
    public PostProcessVolume ppMainV;
    AmbientOcclusion ao;
    Bloom bloom;
    ScreenSpaceReflections ssr;
    public AudioSource audioSource;
    public AudioClip buttonClick;
    public GameObject optionsPanel;
    public GameObject controls;
    public GameObject pauseMenu;
    public AudioMixer mixer;
    public AudioMixer gameMixer;
    public SimpleLUT simpleL;
    public static bool isPaused;
    bool pauseUI;

    public static float audioVolume;
    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume volume = ppMainV.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out ao);
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out ssr);
        Time.timeScale = 1;
        isPaused = !isPaused;
        pauseUI = false;
        optionsPanel.SetActive(false);
        controls.SetActive(false);
        pauseMenu.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;
        string sceneName = currentScene.name;
        if(pauseMenu == true || buildIndex == 0)
        {
            Cursor.visible = true;
        }

        if(buildIndex == 2 && !pauseUI)
        {
            Cursor.visible = false;
        }
        if (buildIndex == 2 && Input.GetKeyDown(KeyCode.Escape))
        {

            Cursor.lockState = CursorLockMode.Confined;
            TimeManager.GamePause = !TimeManager.GamePause;
            pauseMenu.SetActive(false);
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
        audioVolume = sliderValue;
        gameMixer.SetFloat("InGameSound", Mathf.Log10(sliderValue) * 20);

    }
    public void BrightnessAdjuster(float sliderValue)
    {
        simpleL.Brightness = sliderValue;
    }
    public void SharpnessAdjuster(float sliderValue)
    {
        simpleL.Sharpness = sliderValue;
    }
    public void ContrastAdjuster(float sliderValue)
    {
        simpleL.Contrast = sliderValue;
    }
    public void SatAdjuster(float sliderValue)
    {
        simpleL.Saturation = sliderValue;
    }
    public void OpenOptions(bool newValue)
    {
        Cursor.visible = true;
        optionsPanel.SetActive(newValue);

    }
    public void AmbientOcclusion(float sliderValue)
    {
        ao.intensity.value = sliderValue;
    }
    public void BloomAmount(float sliderValue)
    {
        bloom.intensity.value = sliderValue;
    }
    public void SSR(int value)
    {
        if(value == 0)
        {
            ssr.preset.value = ScreenSpaceReflectionPreset.Low;
        }
        if(value == 1)
        {
            ssr.preset.value = ScreenSpaceReflectionPreset.Medium;
        }
        if (value == 2)
        {
            ssr.preset.value = ScreenSpaceReflectionPreset.High;
        }


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
            //make time pause
        }
        if (pauseUI == false)
        {
            pauseMenu.SetActive(false);

        }
    }
    public void ResumeButton()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = !isPaused;
        TimeManager.GamePause = !TimeManager.GamePause;
        pauseMenu.SetActive(false);
        pauseUI = !pauseUI;
        PauseUI();
    }
    public void Invert()
    {
        CamCtrl.YInversion = !CamCtrl.YInversion;
    }
    public void OpenControls(bool newValue)
    {
        Cursor.visible = true;
        controls.SetActive(newValue);

    }
    public void QuitToDesktop()
    {
        Application.Quit();
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
