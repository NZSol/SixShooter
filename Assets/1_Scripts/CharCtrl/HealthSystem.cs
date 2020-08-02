using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    [SerializeField] int health;

    GameObject EndGameUI;

    [SerializeField] Slider HealthMeter;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
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

    }

    void UIUpdate()
    {
        HealthMeter.GetComponent<Slider>().value = health;
    }
}
