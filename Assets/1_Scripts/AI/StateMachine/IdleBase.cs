using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBase : StateMachineBehaviour
{
    GameObject Player;
    float timer = 5;
    AIBase ScriptMaster;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindWithTag("Player");
        ScriptMaster = animator.GetComponent<AIBase>();
        if (TrackBase.searchCount == 3)
        {
            ScriptMaster.animCtrl.SetBool("Tracking", false);
            ScriptMaster.animCtrl.SetBool("Patrolling", true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 traveling = ScriptMaster.travelTo;
        float Dist = Vector3.Distance(animator.transform.position, traveling);
        if (Dist < 3 && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 5;
            ScriptMaster.animCtrl.SetBool("Idling", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ScriptMaster.animCtrl.SetBool("Idling", false);
        if (ScriptMaster.animCtrl.GetInteger("ActState") == 3)
        {
            TrackBase.searchCount++;
        }
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
