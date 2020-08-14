using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] int health;

    [SerializeField] GameObject EndGameUI;
    [SerializeField] GameObject SceneLoader;

    public Animator damageVignetteAnim;
    public Animator cameraShakeAnim;
    [SerializeField] Slider HealthMeter;
    public static bool GameOver;

    bool playerDied;
    public static bool canBeHit = true;
    public bool onRegenHealth;
    bool regenHealth;
    bool healBool;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        EndGameUI.SetActive(false);
        GameOver = false;
    }

    float timer = 5;

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
        if(health <= 0)
        {
            EndGame();
            TimeManager.PauseMenu();
        }
        
        if (health < 100)
        {
            if (timer >= 0 && regenHealth == false)
            {
                timer -= Time.deltaTime;
            }
            else if (timer <= 0 && regenHealth == false)
            {
                if (canBeHit == true)
                {
                    regenHealth = true;
                }
                timer = 5;
            }
        }
        else
        {
            regenHealth = false;
        }
        if (regenHealth == true)
        {
            health++;
        }
        if (GameOver == true)
        {
            EndGame();
        }
    }

    public void healthReduce (int i)
    {
        health -= i;
        StartCoroutine(IFrames());
        regenHealth = false;
        timer = 5;
               
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


        if (health <= 0)
        {
            EndGame();
        }
    }


    IEnumerator IFrames()
    {
        canBeHit = false;
        yield return new WaitForSeconds(2);
        canBeHit = true;
        StopCoroutine(IFrames());
    }

    public void EndGame()
    {
        TimeManager.GamePause = true;
        EndGameUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        //SceneLoader.GetComponent<AsyncLoadFunc>().loadFunc();
    }

    void UIUpdate()
    {
        HealthMeter.GetComponent<Slider>().value = health;
    }
}
