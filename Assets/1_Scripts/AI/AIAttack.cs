using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    [SerializeField] Collider[] colArray;
    attackingScript attack;
    GameObject Player;
    public bool ExitState;
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


    public void Attack()
    {
        foreach (Collider col in colArray)
        {
            col.enabled = true;
        }
    }

    public void AttackEnd()
    {
        foreach (Collider col in colArray)
        {
            col.enabled = false;
        }
        ExitState = true;
        MasterAI.resetAtkRange();
    }
    










}
