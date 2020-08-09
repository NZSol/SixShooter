using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] int health;

    [SerializeField] GameObject EndGameUI;
    [SerializeField] GameObject SceneLoader;

    [SerializeField] Slider HealthMeter;

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
        if (health <= 0)
        {
            EndGame();
        }
    }

    public void healthReduce (int i)
    {
        health -= i;
        if (health <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        EndGameUI.SetActive(true);
        SceneLoader.GetComponent<AsyncLoadFunc>().loadFunc();
    }

    void UIUpdate()
    {
        HealthMeter.GetComponent<Slider>().value = health;
    }
}
