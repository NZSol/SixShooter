using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{

    GameObject player;
    public static Animator animCtrl;

    //Line of Sight
    float playerDist;
    float minDetectRange = 5;
    [SerializeField] float fovRange = 75;
    bool brokenLos;
    


    bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 rayDir = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDir, out hit))
        {
            if ((hit.transform.tag == "Player") && (playerDist <= minDetectRange))
            {
                Debug.DrawRay(transform.position, rayDir, Color.yellow);
                animCtrl.SetInteger("ActState", 2);
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
    }

    private void Update()
    {
        playerDist = Vector3.Distance(player.transform.position, transform.position);

        CanSeePlayer();


        print(animCtrl.GetInteger("ActState"));
    }



}
