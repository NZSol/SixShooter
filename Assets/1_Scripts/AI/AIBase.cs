using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{

    GameObject player;
    Animator animCtrl;

    //Line of Sight
    float playerDist;
    float minDetectRange = 5;
    [SerializeField] float fovRange = 75;

    //Pathing
    [SerializeField] float radius = 20;
    float curTime;
    [SerializeField] float timer;
    NavMeshAgent agent;
    
    bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 rayDir = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDir, out hit))
        {
            if ((hit.transform.tag == "Player") && (playerDist <= minDetectRange))
            {
                Debug.DrawRay(transform.position, rayDir, Color.yellow);
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
                    return true;
                }
                else
                {
                    Debug.DrawRay(transform.position, rayDir, Color.red);
                    return false;
                }
            }
        }
        Debug.DrawRay(transform.position, rayDir, Color.black);
        return false;
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;
        randDir += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, dist, layerMask);

        return navHit.position;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        curTime = timer;
    }

    private void Update()
    {
        playerDist = Vector3.Distance(player.transform.position, transform.position);
        Path();

        CanSeePlayer();
    }


    void Path()
    {
        curTime += Time.deltaTime;
        if (curTime >= timer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, radius, -1);
            print("hit");
            agent.SetDestination(newPos);
            curTime -= timer;
        }
    }

}
