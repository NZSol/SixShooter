using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] int health;

    PlayerSoundEvents damageSoundScript;

    [SerializeField] GameObject EndGameUI;
    [SerializeField] GameObject SceneLoader;

    public Animator damageVignetteAnim;
    public Animator cameraShakeAnim;
    [SerializeField] Slider HealthMeter;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        EndGameUI.SetActive(false);
        damageSoundScript = GetComponentInChildren<PlayerSoundEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
        if(health <= 0)
        {
            EndGame();
            TimeManager.PauseMenu();
        }
    }

    public void healthReduce (int i)
    {
        health -= i;
               
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            cameraShakeAnim.SetTrigger("CameraShake");
        }
        else if (rand == 1)
        {
            cameraShakeAnim.SetTrigger("CameraShake2");
        }
        else if (rand == 2)
        {
            cameraShakeAnim.SetTrigger("CameraShake3");
        }

        cameraShakeAnim.SetTrigger("CameraShake");
        damageVignetteAnim.SetTrigger("PlayerHit");
        damageSoundScript.DamagedSound();

        if (health <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        EndGameUI.SetActive(true);
        //SceneLoader.GetComponent<AsyncLoadFunc>().loadFunc();
    }

    void UIUpdate()
    {
        HealthMeter.GetComponent<Slider>().value = health;
    }
}
