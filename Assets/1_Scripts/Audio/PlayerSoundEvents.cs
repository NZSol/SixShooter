using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerSoundEvents : MonoBehaviour
{
    public AudioSource playerAudioS;
    public AudioSource[] walkAudioS;

    public AudioClip reload;
    public AudioClip firing;
    public AudioClip transitionTo;
    public AudioClip transitionFrom;

    int walkIndex;




    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
    }


    public void Walk()
    {
        walkIndex = Random.Range(0, walkAudioS.Length);

        walkAudioS[walkIndex].PlayOneShot(walkAudioS[walkIndex].clip);
        walkAudioS[walkIndex].volume = 1.5f;
        Debug.Log(walkIndex);
    }

    public void ReloadSound()
    {
        playerAudioS.PlayOneShot(reload);
        playerAudioS.volume = 1.5f;
    }

    public void FiringSound()
    {
        playerAudioS.volume = 1f;
        playerAudioS.PlayOneShot(firing);
        
    }

    public void TransitionToReload()
    {

        playerAudioS.PlayOneShot(transitionTo);
    }

    public void TransitionFromReload()
    {

        playerAudioS.PlayOneShot(transitionFrom);
    }
}
