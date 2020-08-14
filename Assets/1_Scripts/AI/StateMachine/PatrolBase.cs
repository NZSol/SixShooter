using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBase : StateMachineBehaviour
{

    AIBase ScriptMaster;

    [SerializeField] float timer;
    [SerializeField] float speed;
    [SerializeField] float radius;
    float curTime;

    [SerializeField] AnimationClip walk;
    [SerializeField] AnimationClip idle;

    NavMeshAgent agent;

    Animator anim;
    Vector3 newPos;
    //Animator animCtrl;
    //AnimatorOverrideController animOver;

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
        anim = animator;
        agent = animator.GetComponent<NavMeshAgent>();
        curTime = timer;
        animator.SetBool("Patrolling", true);
        ScriptMaster = anim.GetComponent<AIBase>();
        ScriptMaster.travelTo = RandomNavSphere(anim.transform.position, radius, -1);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        newPos = ScriptMaster.travelTo;
        float Dist = Vector3.Distance(anim.transform.position, newPos);
        //move towards direction
        if (agent.velocity == Vector3.zero && Dist < 3)
        {
            ScriptMaster.animCtrl.SetBool("Idling", true);
            Debug.Log(ScriptMaster.animCtrl.GetBool("Idling"));
        }
        else
        {
            ScriptMaster.animCtrl.SetBool("Idling", false);
        }
        agent.SetDestination(newPos);
        Vector3 rayDir = newPos - anim.transform.position;
        Debug.DrawRay(anim.transform.position, rayDir, Color.cyan);

    }

    void TimeCount()
    {
        curTime += Time.deltaTime;
        if(curTime >= timer)
        {
            //Findhit();
            Vector3 newPos = RandomNavSphere(anim.transform.position, radius, -1);
            agent.SetDestination(newPos);
            curTime -= timer;
        }
    }


    void Findhit()
    {
        RaycastHit hit;
        if (Physics.Raycast(RandomNavSphere(new Vector3(agent.transform.position.x, agent.transform.position.y + 5, agent.transform.position.z), 5, 1 << 0), Vector3.down, out hit, 10, 1 << 0))
        {
            if (hit.transform.gameObject.layer == 1 << 0)
            {
                Vector3 newPos = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
                agent.SetDestination(newPos);
            }
            else
            {
                Findhit();
            }
        }
    }


    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("Patrolling", false);
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
