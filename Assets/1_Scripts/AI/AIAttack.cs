using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    attackingScript attack;
    GameObject Player;
    public bool ExitState;
    float Dist;
    Animator anim;
    [SerializeField] attackingScript attackState;
    AIBase MasterAI;
    [SerializeField] float dmgRange;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        MasterAI = GetComponent<AIBase>();
    }

    AnimatorStateInfo currentState;
    AnimatorStateInfo desiredState;
    AnimationState animState;
    private void Update()
    {
        if (MasterAI.animCtrl.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            currentState = MasterAI.animCtrl.GetCurrentAnimatorStateInfo(0);
        }
        if (attackState == null)
        {
            //animState = MasterAI.animCtrl.GetCurrentAnimatorStateInfo(0).IsName("Attacking");
        }
        //Dist = attackState.distance;
        print(currentState.shortNameHash);
    }

    public void DoCoroutine()
    {
        StartCoroutine(PlayerAttack());
        print("CheckingCoroutine");
    }

    public IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(3);
        if(Dist < dmgRange)
        {
            Player.GetComponent<HealthSystem>().healthReduce(i: 15);
        }
        ExitState = true;
    }

}
