using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public static bool bombRigged;

    [SerializeField] Camera cam;
    [SerializeField] KeyCode Interact;
    Vector3 forward;
    public bool coroutineBool, endCoroutineBool;
    bool inOil;
    public bool hitBool;
    [SerializeField] float interCheckRange;

    //UI
    [SerializeField] Image img;
    [SerializeField] Slider slide;
    float slideVal;
    [SerializeField] GameObject bomb;
    float Dist;
    [SerializeField] float rangeFromBomb;
    [SerializeField] float bombRange;

    void Start()
    {
        img.enabled = false;
        slide.gameObject.SetActive(false);
        slideVal = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Dist = Vector3.Distance(bomb.transform.position, gameObject.transform.position);
        slide.value = slideVal;

        if (slideVal <= 0 || slideVal >= 5)
        {
            slide.gameObject.SetActive(false);
        }
        else
        {
            slide.gameObject.SetActive(true);
        }

        RaycastHit hit;
        forward = cam.transform.forward;
        Vector3 rayDir = bomb.transform.position - transform.position;
        Debug.DrawRay(transform.position, forward * interCheckRange, Color.cyan);

        if (Physics.Raycast(transform.position, forward, out hit, interCheckRange, 1 << 9))
        {
            if (Input.GetKeyDown(Interact))
            {
                if (hit.transform.tag == "door")
                {
                    print("checking");
                    hit.transform.gameObject.GetComponent<DoorSwing>().triggered = !hit.transform.gameObject.GetComponent<DoorSwing>().triggered;
                    FindObjectOfType<AudioManager>().Play("Door");
                }
            }
        }
        if (Physics.Raycast(transform.position, rayDir, out hit, 1 << 9))
        {
            Debug.DrawRay(transform.position, rayDir, Color.yellow);
            if (Input.GetKey(Interact) && Dist <= bombRange)
            {
                if (hit.transform.tag == "endGame" && slideVal <= 5)
                {
                    print("acting");
                    slideVal += Time.deltaTime;
                    if (slideVal > 5)
                    {
                        slideVal = 5;
                    }
                }
                else
                {
                    slideVal = 0;
                }
            }
            if (slideVal > 0 || slideVal < 5)
            {
                if (!Input.GetKey(Interact))
                {
                    slideVal = 0;
                }
            }
        }

        
        if(Dist < rangeFromBomb)
        {
            img.enabled = true;
        }
        else
        {
            img.enabled = false;
        }

        print(slide.value + "slide.Value");
        print(slideVal + "SlideVal");
        if(Dist < rangeFromBomb)
        {
            img.enabled = true;
        }
        else
        {
            img.enabled = false;
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
            //print(hit.transform.gameObject.layer);
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
