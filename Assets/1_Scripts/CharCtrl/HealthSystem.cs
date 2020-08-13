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

    public static bool canBeHit = true;
    public bool onRegenHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        EndGameUI.SetActive(false);
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

        if (health<100 && onRegenHealth == false)
        {
            StartCoroutine("RegenDelay");
        }
    }

    public void healthReduce (int i)
    {
        health -= i;
        StopCoroutine(RegenHealth());
        StartCoroutine(RegenDelay());
        StartCoroutine(IFrames());
               
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

    public IEnumerator RegenDelay()
    {
        yield return new WaitForSeconds(5);
        onRegenHealth = true;
        StartCoroutine("RegenHealth");
    }

    public IEnumerator RegenHealth()
    {
        if (onRegenHealth == true)
        {
            health = Mathf.Clamp(health + 1, -100, 100);
            yield return new WaitForSeconds(1);
            onRegenHealth = false;
        }
    }

    IEnumerator IFrames()
    {
        canBeHit = false;
        yield return new WaitForSeconds(2);
        canBeHit = true;
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
