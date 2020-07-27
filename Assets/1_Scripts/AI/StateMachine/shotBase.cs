using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shotBase : StateMachineBehaviour
{


    GameObject player;
    Vector3 playerPos;
    Vector3 targDest;

    NavMeshAgent agent;
    Animator anim;

    float timer;
    float curTimer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = animator.GetComponent<NavMeshAgent>();

        playerPos = player.transform.position;
        anim = animator;
        playerPos = player.transform.position;
        targDest = playerPos;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(targDest);
        float Dist = Vector3.Distance(playerPos, anim.transform.position);
        if (Dist <= 1f)
        {
            curTimer += Time.deltaTime;
            if (curTimer >= timer)
            {
                curTimer -= timer;
                anim.gameObject.GetComponent<Animator>().SetBool("Shot", false);
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
