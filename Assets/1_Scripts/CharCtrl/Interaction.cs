using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] KeyCode Interact;
    Vector3 forward;
    public bool coroutineBool, endCoroutineBool;
    bool inOil;
    public bool hitBool;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        forward = cam.transform.forward;
        Debug.DrawRay(transform.position, forward * 3, Color.cyan);
        if (Physics.Raycast(transform.position, forward, out hit, 3, 1 << 9))
        {
            if (Input.GetKeyDown(Interact))
            {
                if(hit.transform.tag == "door")
                {
                    hit.transform.gameObject.GetComponent<DoorSwing>().triggered = !hit.transform.gameObject.GetComponent<DoorSwing>().triggered;
                    FindObjectOfType<AudioManager>().Play("Door");
                }
                
                if(hit.transform.tag == "emit")
                {
                    hit.transform.gameObject.GetComponent<lightFlash>().check = !hit.transform.gameObject.GetComponent<lightFlash>().check;
                }
            }
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2))
        {
            if (hit.transform.gameObject.layer == 13 && hitBool == false)
            {
                hitBool = true;
                coroutineBool = true;
                endCoroutineBool = false;
                inOil = true;
            }
            else
            {
                inOil = false;
                endCoroutineBool = true;
                hitBool = false;
            }
            print(hit.transform.gameObject.layer);
        }


        if (coroutineBool == true && endCoroutineBool == false)
        {
            coroutineBool = false;
            StartCoroutine(PlayerDamage());
        }
        else
        {
            StopCoroutine(PlayerDamage());
        }
    }


    IEnumerator PlayerDamage()
    {
        print("checking");
        do
        {
            gameObject.GetComponent<HealthSystem>().healthReduce(i: 5);
            yield return new WaitForSeconds(2);
        }
        while (inOil);
    }

}
