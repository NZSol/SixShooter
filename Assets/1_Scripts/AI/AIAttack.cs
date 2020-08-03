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

    AnimatorStateInfo currentState;
    private void Update()
    {
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
        GetComponent<BoxCollider>().enabled = true;
        Player.GetComponent<HealthSystem>().healthReduce(i: 15);
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        ExitState = true;
    }

    private void OnTriggerEnter(Collider other)
    {
     if (other.tag == "Player")
        {
            Player.GetComponent<HealthSystem>().healthReduce(i: 15);
        }
    }

}
