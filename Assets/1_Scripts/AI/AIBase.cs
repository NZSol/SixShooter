using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{
    GameObject player;
    public Animator animCtrl;

    //Line of Sight
    float playerDist;
    float minDetectRange = 10;
    [SerializeField] float attackRange = 3f;
    float baseAttackRange;
    [SerializeField] float fovRange = 75;
    bool brokenLos;
    Spawning spawnScript;

    int health;


    bool CanSeePlayer()
    {
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
        animCtrl = GetComponent<Animator>();
        health = 10;
        spawnScript = GetComponentInParent<Spawning>();
        baseAttackRange = attackRange;
        disableBones();
    }

    private void Update()
    {
        playerDist = Vector3.Distance(player.transform.position, transform.position);

        CanSeePlayer();
        HealthCheck();

    }

    void HealthCheck()
    {
        if (health <= 0)
        {
            spawnScript.aiCharList.Remove(gameObject);
            Destroy(GetComponent<Animator>());
            enableBones();
        }
    }


    Collider[] colliders;
    public List<Collider> RagdollParts = new List<Collider>();
    public List<Collider> ColliderParts = new List<Collider>();

    void enableBones()
    {
        colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
        
    }

    void disableBones()
    {
        colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            if (col.gameObject == this.gameObject || col.tag == "listDontAdd" || col.tag == "critPoint" || col.tag == "regDamage" || col.material == null)
            {
                ColliderParts.Add(col);
            }
            else
            {
                RagdollParts.Add(col);
            }
        }

        colliders = RagdollParts.ToArray();
        foreach (Collider col in RagdollParts)
        {
            col.enabled = false;
        }
    }


    public void Damage(int i)
    {
        health -= i;
        animCtrl.SetBool("Shot", true);
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
