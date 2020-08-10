using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SoundEvents : MonoBehaviour
{
    

    public AudioSource[] walkAudioS;
    public AudioSource reloadAudioS;
    public AudioClip reload;
    public AudioSource firingS;
    public AudioClip firing;
    public AudioSource transitionToS;
    public AudioClip transitionTo;
    public AudioSource transitionFromS;
    public AudioClip transitionFrom;

    int index;




    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
    }


    public void Walk()
    {
        index = Random.Range(0, walkAudioS.Length);

        walkAudioS[index].PlayOneShot(walkAudioS[index].clip);
        Debug.Log(index);
    }

    public void ReloadSound()
    {
        reloadAudioS.PlayOneShot(reload);
    }

    public void FiringSound()
    {
        reloadAudioS.volume = 0.5f;
        reloadAudioS.PlayOneShot(firing);
        
    }

    public void TransitionToReload()
    {

        transitionToS.PlayOneShot(transitionTo);
    }

    public void TransitionFromReload()
    {

        transitionFromS.PlayOneShot(transitionFrom);
    }
}
