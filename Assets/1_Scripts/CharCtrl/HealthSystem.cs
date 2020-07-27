using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    float health;

    GameObject EndGameUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
