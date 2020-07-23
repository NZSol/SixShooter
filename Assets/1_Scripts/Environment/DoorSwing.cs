using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwing : MonoBehaviour
{

    public bool triggered;
    [SerializeField] AnimationCurve falloffMult;
    [SerializeField] bool openInOut;
    Vector3 targetOpen;
    Vector3 targetClose;
    float falloff;
    float swingForce;
    [SerializeField] float multiplier;

    void Start()
    {
        if (openInOut == true)
        {
            targetOpen = new Vector3(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
        }
        else
        {
            targetOpen = new Vector3(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z);
        }
        targetClose = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        swingForce = falloffMult.Evaluate(falloff);
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered == true)
        {
            DoorOpen();
        }
        else
        {
            DoorClose();
        }
    }

    void DoorOpen()
    {
        Quaternion target = Quaternion.Euler(targetOpen);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, swingForce * (Time.deltaTime * multiplier));
    }

    void DoorClose()
    {
        Quaternion target = Quaternion.Euler(targetClose);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, swingForce * (Time.deltaTime * multiplier));
    }

    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.tag == "Player")
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            triggered = !triggered;
    //            print(triggered);
    //        }
    //    }
    //}
    public void OpenDoor()
    {
        triggered = !triggered;
    }

}
