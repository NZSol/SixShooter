using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TrackBase : StateMachineBehaviour
{
    GameObject player;
    private AIBase ScriptMaster;
    Vector3 playerPos;
    Vector3 targDest;

    NavMeshAgent agent;

    bool tracking;
    public static bool sightBreak;
    float targDist;
    float timer = 5;
    float curTime;
    float searchRadius = 5;
    int searchCount;

    Animator anim;

    Vector3 SearchArea(Vector3 origin, float dist, int layerMask)
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
        player = GameObject.FindGameObjectWithTag("Player");
        agent = animator.GetComponent<NavMeshAgent>();

        tracking = true;
        playerPos = player.transform.position;
        curTime = 0;
        anim = animator;
        ScriptMaster = animator.GetComponent<AIBase>();
        animator.SetBool("Tracking", true);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (tracking)
        {
            Track();
        }
        else
        {
            Search();
        }
        if(searchCount == 3)
        {
            ScriptMaster.animCtrl.SetInteger("ActState", 1);
            ScriptMaster.animCtrl.SetBool("Tracking", false);
        }
        if(agent.velocity == Vector3.zero)
        {
            animator.SetBool("Idling", true);
        }
        else
        {
            animator.SetBool("Idling", true);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        searchCount = 0;
    }


    void Track()
    {

        targDist = Vector3.Distance(playerPos, anim.transform.position);

        agent.SetDestination(playerPos);
        if (targDist < 1)
        {
            curTime += Time.deltaTime;
        }
        if (curTime >= timer)
        {
            tracking = false;
            curTime = 0;
        }
    }


    void Search()
    {
        ScriptMaster.travelTo = targDest;
        targDist = Vector3.Distance(targDest, anim.transform.position);

        if(targDest == null)
        {
            targDest = SearchArea(playerPos, searchRadius, -1);
        }

        if (targDist < 1)
        {
            curTime += Time.deltaTime;
        }
        if(curTime >= 5)
        {
            targDest = SearchArea(playerPos, searchRadius, -1);
            curTime = 0;
            searchCount++;
        }


        agent.SetDestination(targDest);
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
