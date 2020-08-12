using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundEvents : MonoBehaviour
{
    public AudioSource[] walkAudioS;
    public AudioSource[] attackAudioS;
    public AudioSource idleAudioS;
    public AudioSource[] deathAudioS;

    public AudioClip idle;

    int attackindex;
    int walkindex;
    int deathindex;




    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        idleAudioS.Play();

    }


    public void Attack()
    {
        attackindex = Random.Range(0, attackAudioS.Length);

        attackAudioS[attackindex].PlayOneShot(attackAudioS[attackindex].clip);
        attackAudioS[attackindex].volume = 1.0f;
        Debug.Log(attackindex);
    }

    public void WalkSound()
    {
        walkindex = Random.Range(0, walkAudioS.Length);

        walkAudioS[walkindex].PlayOneShot(walkAudioS[walkindex].clip);
    }

    public void DeathSound()
    {
        deathindex = Random.Range(0, deathAudioS.Length);

        deathAudioS[deathindex].PlayOneShot(deathAudioS[deathindex].clip);
    }
}
