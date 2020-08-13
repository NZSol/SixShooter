using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{
    GameObject player;
    public Animator animCtrl;

    EnemySoundEvents enemyDeathScript;
    

    //Line of Sight
    float playerDist;
    float minDetectRange = 10;
    [SerializeField] float attackRange;
    float baseAttackRange;
    [SerializeField] float fovRange = 75;
    bool brokenLos;
    Spawning spawnScript;

    int health;
    Vector3 EndPos, endPosDir;

    bool CanSeePlayer()
    {
        if (EndPos != Vector3.zero)
        {
            endPosDir = EndPos - transform.position;
        }
        RaycastHit hit;
        Vector3 rayDir = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDir, out hit))
        {
            if ((hit.transform.tag == "Player") && (playerDist <= minDetectRange && playerDist >= attackRange))
            {
                Debug.DrawRay(transform.position, rayDir, Color.yellow);
                animCtrl.SetInteger("ActState", 2);
                return true;
            }
            else if ((hit.transform.tag == "Player") && (playerDist <= attackRange))
            {
                Debug.DrawRay(transform.position, rayDir, Color.blue);
                animCtrl.SetBool("Attacking", true);
                return true;
            }
        }
        if(Vector3.Angle(rayDir, transform.forward) < fovRange)
        {
            if (Physics.Raycast(transform.position, rayDir, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    Debug.DrawRay(transform.position, rayDir, Color.green);
                    animCtrl.SetInteger("ActState", 2);
                    return true;
                }
                else
                {
                    Debug.DrawRay(transform.position, rayDir, Color.red);
                    print(hit);
                    animCtrl.SetInteger("ActState", 3);

                    return false;
                }
            }
        }
        Debug.DrawRay(transform.position, rayDir, Color.black);
        return false;
    }


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyDeathScript = GetComponent<EnemySoundEvents>();
        animCtrl = GetComponent<Animator>();
        health = 10;
        spawnScript = GetComponentInParent<Spawning>();
        populateLists();
        baseAttackRange = attackRange;
    }

    private void Update()
    {
        playerDist = Vector3.Distance(player.transform.position, transform.position);

        CanSeePlayer();
        HealthCheck();


    }

    void HealthCheck()
    {
        int val = 0;
        if (health <= 0)
        {
            if(spawnScript)
            {
                spawnScript.aiCharList.Remove(gameObject);
            }
            enableRagdollBones();
            Destroy(animCtrl);
            if (val < 1)
            {
                val++;
            }
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(this);
        }
    }


    Collider[] colliders;
    public List<Collider> RagdollParts = new List<Collider>();
    public List<Collider> ColliderParts = new List<Collider>();


    void populateLists()
    {
        colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            if (col.gameObject == this.gameObject || col.tag == "listDontAdd" || col.tag == "critPoint" || col.tag == "regDamage")
            {
                ColliderParts.Add(col);
            }
            else
            {
                RagdollParts.Add(col);
            }
        }
        StartCoroutine(disableRagdollBones());
    }

    IEnumerator disableRagdollBones()
    {
        yield return new WaitForSeconds(2);
        disableBones();
    }

    void enableRagdollBones()
    {
        foreach (Collider col in RagdollParts)
        {
            col.enabled = true;
            if (col.GetComponent<Rigidbody>() != null)
            {
                col.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        foreach (Collider col in ColliderParts)
        {
            col.enabled = false;
        }

        
    }

    void disableBones()
    {
        foreach (Collider col in RagdollParts)
        {
            col.enabled = false;
            if (col.GetComponent<Rigidbody>() != null)
            {
                col.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        foreach(Collider col in ColliderParts)
        {
            col.enabled = true;
        }
    }


    public void Damage(int i)
    {
        print(i + " damaging AI");
        health -= i;
        if (animCtrl.GetBool("Shot") == false || animCtrl.GetBool("Following") == false)
        {
            enemyDeathScript.DeathSound();
            animCtrl.SetBool("Shot", true);
        }
    }

    public void resetAtkRange()
    {
        StartCoroutine(ATKRange());
    }

    IEnumerator ATKRange()
    {
        attackRange = 0;
        yield return new WaitForSeconds(0.5f);
        attackRange = baseAttackRange;
    }
}
