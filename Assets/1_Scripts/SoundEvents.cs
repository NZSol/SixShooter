using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvents : MonoBehaviour
{
    

    public AudioSource[] audioS;
    int index;




    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
    }


    public void Walk()
    {
        index = Random.Range(0, audioS.Length);

        audioS[index].PlayOneShot(audioS[index].clip);
        Debug.Log(index);
    }
}
