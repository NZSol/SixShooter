using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeExtender : MonoBehaviour
{

    public Animator extendBridge;
    private bool extended; 

    // Start is called before the first frame update
    void Start()
    {
        extended = false;
        extendBridge.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
