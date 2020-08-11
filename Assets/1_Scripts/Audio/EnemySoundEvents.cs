using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundEvents : MonoBehaviour
{
    public AudioSource walkAudioS;
    public AudioSource[] attackAudioS;
    public AudioSource idleAudioS;

    public AudioClip walk;
    public AudioClip idle;

    int index;




    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        idleAudioS.Play();

    }


    public void Attack()
    {
        index = Random.Range(0, attackAudioS.Length);

        attackAudioS[index].PlayOneShot(attackAudioS[index].clip);
        attackAudioS[index].volume = 1.0f;
        Debug.Log(index);
    }

    public void WalkSound()
    {
        //walkAudioS.PlayOneShot(walk);
        //walkAudioS.volume = 1.0f;
    }
}
