using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class GameStartAfterIntro : MonoBehaviour
{
    public VideoPlayer intro;
    // Start is called before the first frame update
    void Start()
    {

        intro.SetDirectAudioVolume(0, ButtonClick.audioVolume);
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
    }
    IEnumerator StartGame()
    {

        yield return new WaitForSeconds(25);
        SceneManager.LoadScene(2);
    }
}
