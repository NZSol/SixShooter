using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBase : StateMachineBehaviour
{

    

    [SerializeField] float timer;
    [SerializeField] float speed;
    [SerializeField] float radius;
    float curTime;

    NavMeshAgent agent;

    Animator anim;

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;
        randDir += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, dist, layerMask);

        return navHit.position;
    }



    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        curTime = timer;
        agent = animator.GetComponent<NavMeshAgent>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //move towards direction
        anim = animator;
        TimeCount();
        
    }

    void TimeCount()
    {
        curTime += Time.deltaTime;
        if(curTime >= timer)
        {
            Vector3 newPos = RandomNavSphere(anim.transform.position, radius, -1);
            agent.SetDestination(newPos);
            curTime -= timer;
        }
    }





    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
