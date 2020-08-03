using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawning : MonoBehaviour
{
    [SerializeField] GameObject AI;
    public List<GameObject> aiCharList;
    int aiCount = 0;
    [SerializeField] int maxCount;
    [SerializeField] int maxSpawnNumber;

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
        spawnTimer();
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

    void RandomRecast()
    {
        Vector3 RayFrom = new Vector3(target.x, target.y + 1000, target.z);
        RaycastHit hit;

        if (Physics.Raycast(RayFrom, Vector3.down * 2000, out hit, Mathf.Infinity, layerMask: 1 << 0))
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
