using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEvents : MonoBehaviour
{
    public AudioSource playerAudioS;
    public AudioSource[] walkAudioS;
    public AudioSource[] damagedAudioS;
    public AudioClip reload;
    public AudioClip firing;
    public AudioClip transitionTo;
    public AudioClip transitionFrom;

    int walkIndex;
    int damageIndex;




    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
    }


    public void Walk()
    {
        walkIndex = Random.Range(0, walkAudioS.Length);

        walkAudioS[walkIndex].PlayOneShot(walkAudioS[walkIndex].clip);
        walkAudioS[walkIndex].volume = 1.5f;
        //Debug.Log(walkIndex);
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

    public void DamagedSound()
    {
        damageIndex = Random.Range(0, damagedAudioS.Length);
        damagedAudioS[damageIndex].PlayOneShot(damagedAudioS[damageIndex].clip);
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
