using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] KeyCode Interact;
    Vector3 forward;
    public bool coroutineBool, endCoroutineBool;
    bool inOil;
    public bool hitBool;

    //UI
    [SerializeField] Image img;
    [SerializeField] Slider slide;
    float slideVal;
    [SerializeField] GameObject bomb;
    float Dist;
    [SerializeField] float rangeFromBomb;

    // Update is called once per frame
    void Update()
    {
        Dist = Vector3.Distance(bomb.transform.position, gameObject.transform.position);

        RaycastHit hit;
        forward = cam.transform.forward;
        Debug.DrawRay(transform.position, forward * 3, Color.cyan);
        if (Physics.Raycast(transform.position, forward, out hit, 3, 1 << 9))
        {
            if (Input.GetKeyDown(Interact))
            {
                if (hit.transform.tag == "door")
                {
                    hit.transform.gameObject.GetComponent<DoorSwing>().triggered = !hit.transform.gameObject.GetComponent<DoorSwing>().triggered;
                    FindObjectOfType<AudioManager>().Play("Door");
                }
            }

            if (Input.GetKeyDown(Interact))
            {
                if (hit.transform.tag == "endGame" && slideVal <= 5)
                {
                    img.enabled = true;
                    slideVal += Time.deltaTime;
                    slide.value = slideVal;
                    if (slideVal > 5)
                    {
                        slideVal = 5;
                    }
                }
                else
                {
                    slideVal = 0;
                    img.enabled = false;
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
