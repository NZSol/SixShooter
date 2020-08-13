using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnSound : MonoBehaviour
{
    public GameObject[] AI;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AI.Length; i++)
        {

            AI[i].SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            for (int i = 0; i < AI.Length; i++)
            {
                AI[i].SetActive(true);
            }
        }
    }
}
