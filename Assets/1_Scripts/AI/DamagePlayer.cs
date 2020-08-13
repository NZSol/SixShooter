using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    GameObject Player;
    AIAttack attackMaster;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        attackMaster = GetComponentInParent<AIAttack>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && HealthSystem.canBeHit == true)
        {
            print("triggered");
            Player.GetComponent<HealthSystem>().healthReduce(i: 33);
            attackMaster.AttackEnd();
        }
    }
}
