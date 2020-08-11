using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SoundEvents : MonoBehaviour
{

            
       
        public AudioClip reload;
        public AudioClip firing;
        public AudioClip transitionTo;
        public AudioClip transitionFrom;

        
    public AudioSource playerAudioS;
    public AudioSource[] walkAudioS;
    int index;




    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
    }


    public void Walk()
    {
        index = Random.Range(0, walkAudioS.Length);

        walkAudioS[index].PlayOneShot(walkAudioS[index].clip);
        walkAudioS[index].volume = 0.5f;
        Debug.Log(index);
    }

    public void ReloadSound()
    {
        playerAudioS.PlayOneShot(reload);
        playerAudioS.volume = 1.5f;
    }

    public void FiringSound()
    {
        playerAudioS.volume = 0.5f;
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
