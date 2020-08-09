using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBones : MonoBehaviour
{
    Collider[] colliders;
    public List<Collider> RagdollParts = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        setRagdollParts();

    }
    public void setRagdollParts()
    {
        colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            if (col.gameObject != this.gameObject)
            {
                col.enabled = false;
                RagdollParts.Add(col);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
