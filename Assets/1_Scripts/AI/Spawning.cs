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

    Vector3 SpawnPos(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;
        randDir += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, dist, layerMask);

        return navHit.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        aiCharList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        print(aiCharList.Count);
        while (aiCharList.Count < maxCount)
        {
            GameObject newAI = Instantiate(AI, SpawnPos(new Vector3(transform.position.x, 0, transform.position.z), 5, 1 << 0), transform.rotation, gameObject.transform);
            aiCharList.Add(newAI);
        }

    }

}
