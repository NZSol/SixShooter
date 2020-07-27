﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{

    GameObject player;
    public static Animator animCtrl;

    //Line of Sight
    float playerDist;
    float minDetectRange = 10;
    float attackRange = 3f;
    [SerializeField] float fovRange = 75;
    bool brokenLos;

    int health;


    bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 rayDir = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDir, out hit))
        {
            if ((hit.transform.tag == "Player") && (playerDist <= minDetectRange && playerDist >= attackRange))
            {
                Debug.DrawRay(transform.position, rayDir, Color.yellow);
                animCtrl.SetInteger("ActState", 2);
                return true;
            }
            else if ((hit.transform.tag == "Player") && (playerDist <= attackRange))
            {
                Debug.DrawRay(transform.position, rayDir, Color.blue);
                animCtrl.SetBool("Attacking", true);
                return true;
            }
        }
        if(Vector3.Angle(rayDir, transform.forward) < fovRange)
        {
            if (Physics.Raycast(transform.position, rayDir, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    Debug.DrawRay(transform.position, rayDir, Color.green);
                    animCtrl.SetInteger("ActState", 2);
                    return true;
                }
                else
                {
                    Debug.DrawRay(transform.position, rayDir, Color.red);
                    animCtrl.SetInteger("ActState", 3);

                    return false;
                }
            }
        }
        Debug.DrawRay(transform.position, rayDir, Color.black);
        return false;
    }


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        animCtrl = GetComponent<Animator>();
        health = 10;
    }

    private void Update()
    {
        playerDist = Vector3.Distance(player.transform.position, transform.position);

        CanSeePlayer();
        HealthCheck();

        print(animCtrl.GetInteger("ActState"));
        print(animCtrl.GetBool("Shot"));
    }

    void HealthCheck()
    {
        if (health <= 0)
        {
            gameObject.GetComponent<AIBase>().enabled = false;
            Destroy(gameObject);
        }
    }



    public void Damage(int i)
    {
        health -= i;
        animCtrl.SetBool("Shot", true);
    }
}
