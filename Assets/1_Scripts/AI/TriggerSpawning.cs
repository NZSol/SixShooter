using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TriggerSpawning : MonoBehaviour
{
    [SerializeField] GameObject AI;
    public List<GameObject> aiCharList;
    int aiCount = 0;
    [SerializeField] int maxCount;
    [SerializeField] int maxSpawnNumber;
    bool initSpawn;
    bool spawnEnable;
    bool initSpawned;

    //Vector3 SpawnPos(Vector3 origin, float dist, int layerMask)
    //{
    //    Vector3 randDir = Random.insideUnitSphere * dist;
    //    randDir += origin;

    //    NavMeshHit navHit;
    //    NavMesh.SamplePosition(randDir, out navHit, dist, layerMask);

    //    return navHit.position;
    //}

    Vector3 RandomPoint(Vector3 center)
    {
        Vector3 result = center;
        for (int i = 0; i < 30;)
        {
            Vector2 TargetPoint = Random.insideUnitCircle * Random.Range(1, 5);
            Vector3 randomPoint = center + new Vector3(TargetPoint.x, 0, TargetPoint.y);

            return randomPoint;
        }


        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        aiCharList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnable == true && initSpawned)
        {
            spawnTimer();
        }
        if(initSpawn == true)
        {
            DoSpawn();
        }
    }



    [SerializeField] float timeToSpawn;
    float timer;
    bool canSpawn;

    void spawnTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += timeToSpawn;
            canSpawn = true;
            Spawn();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            initSpawn = true;
        }
    }

    Vector3 target;

    void Spawn()
    {
        target = RandomPoint(transform.position);
        RandomRecast();
        while (aiCharList.Count < maxCount && aiCount <= maxSpawnNumber && canSpawn)
        {
            GameObject newAI = Instantiate(AI, target, transform.rotation, gameObject.transform);
            aiCharList.Add(newAI);
            aiCount++;
            canSpawn = false;
        }
    }

    void DoSpawn()
    {
        target = RandomPoint(transform.position);
        RandomRecast();
        do
        {
            GameObject newAI = Instantiate(AI, target, transform.rotation, gameObject.transform);
            aiCharList.Add(newAI);
            aiCount++;
        }
        while (aiCharList.Count < maxCount);
        if(aiCharList.Count >= maxCount)
        {
            initSpawn = false;
            initSpawned = true;
        }
    }

    void RandomRecast()
    {
        Vector3 RayFrom = new Vector3(target.x, target.y + 5, target.z);
        RaycastHit hit;

        if (Physics.Raycast(RayFrom, Vector3.down * 10, out hit, Mathf.Infinity, layerMask: 1 << 0))
        {
            target = new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z);
        }
        else
        {
            target = RandomPoint(transform.position);
            RandomRecast();
        }

    }

}
