using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
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


    public void DoCoroutine()
    {
        print("CheckingCoroutine");
        StartCoroutine(PlayerAttack());
    }

    public IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(3);
        GetComponent<BoxCollider>().enabled = true;
        //Player.GetComponent<HealthSystem>().healthReduce(i: 15);
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider>().enabled = false;
        ExitState = true;
        MasterAI.resetAtkRange();
    }

    private void OnTriggerEnter(Collider col)
    {
     if (col.tag == "Player")
        {
            print("triggered");
            Player.GetComponent<HealthSystem>().healthReduce(i: 15);
        }
    }

}
