using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClick;
    public GameObject optionsPanel;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void OpenOptions(bool newValue)
    {
        optionsPanel.SetActive(newValue);

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
