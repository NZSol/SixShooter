using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public AudioSource explosion;
    public AudioSource boat;
    public Text thanks;

    private bool fadeInText = false;
    private bool fadeOutSound = false;
    // Start is called before the first frame update
    void Start()
    {
        fadeInText = false;
        fadeOutSound = false;
        boat.volume = 0.075f;
        explosion.Stop();
        boat.Stop();
        thanks.color = new Color(1, 1, 1, 0);
        Invoke("PlayBoat",1);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeInText)
        {
            thanks.color = new Color(1, 1, 1, Mathf.Lerp(0, 100, 1f));
        }
        if (fadeOutSound)
        {
            boat.volume = Mathf.Lerp(boat.volume, 0, 0.005f);
        }

    }
    void PlayBoat()
    {
        boat.Play();
        Invoke("PlayExplosions",3);
        fadeInText = true;

    }
    void PlayExplosions()
    {
        explosion.Play();
        Invoke("LoadMenu",6);

        Invoke("FadeOutSound", 3);
    }
    void FadeOutSound()
    {
        fadeOutSound = true;
    }
    void LoadMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(0);
    }
}
