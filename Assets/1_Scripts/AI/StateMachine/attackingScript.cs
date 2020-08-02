using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class attackingScript : StateMachineBehaviour
{

    public GameObject player;
    public Animator anim;
    public bool attacking;
    public float distance;
    bool exitState = false;
    
    AIBase scriptMaster;
    public attackingScript attackScript;
    AIAttack MonoState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackScript = animator.GetComponent<attackingScript>();
        anim = animator;
        player = GameObject.FindGameObjectWithTag("Player");
        attacking = true;
        scriptMaster = animator.GetComponent<AIBase>();
        MonoState = animator.GetComponent<AIAttack>();
        MonoState.DoCoroutine();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        exitState = MonoState.ExitState;
        distance = Vector3.Distance(player.transform.position, anim.transform.position);

        if (exitState == true)
        {
            scriptMaster.animCtrl.SetBool("Attacking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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