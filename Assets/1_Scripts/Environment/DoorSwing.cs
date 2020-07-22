using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwing : MonoBehaviour
{

    public static bool triggered;
    [SerializeField] AnimationCurve falloffMult;
    Vector3 targetOpen;
    Vector3 targetClose;
    float falloff;
    float swingForce;

    void Start()
    {
        targetOpen = new Vector3(0, -130, 0);
        targetClose = Vector3.zero;
        swingForce = falloffMult.Evaluate(falloff);
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered == true)
        {
            DoorOpen();
            falloff += Time.deltaTime;
        }
        else
        {
            DoorClose();
            falloff += Time.deltaTime;
        }
    }

    void DoorOpen()
    {
        Quaternion target = Quaternion.Euler(targetOpen);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, swingForce);
    }

    void DoorClose()
    {
        Quaternion target = Quaternion.Euler(targetClose);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, swingForce);
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                triggered = !triggered;
                print(triggered);
            }
            print("in area");
        }
    }
}
