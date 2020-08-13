﻿using System.Collections;
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
    [SerializeField] Text plantTheBomb;
    [SerializeField] Text reachThePlatform;
    [SerializeField] Slider slide;
    float slideVal;
    [SerializeField] GameObject bomb;
    float Dist;
    [SerializeField] float rangeFromBomb;
    [SerializeField] float bombRange;

    void Start()
    {
        reachThePlatform.gameObject.SetActive(true);
        plantTheBomb.gameObject.SetActive(false);
        slide.gameObject.SetActive(false);
        slideVal = 0;
        timer.gameObject.SetActive(false);
        bombRigged = false;
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
                        bombRigged = true;
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


        if (Dist < rangeFromBomb && bombRigged == false)
        {
            reachThePlatform.gameObject.SetActive(false);
            plantTheBomb.gameObject.SetActive(true);
        }
        else
        {
            plantTheBomb.gameObject.SetActive(false);
        }

        print(slide.value + "slide.Value");
        print(slideVal + "SlideVal");



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


        if(bombRigged == true)
        {
            endgameTimer();
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

    public float endgameTime;
    [SerializeField] Text timer;
    float minutes;
    float seconds;

    void endgameTimer()
    {
        timer.gameObject.SetActive(true);
        if (endgameTime > 0)
        {
            endgameTime -= Time.deltaTime;
        }
        else
        {
            endgameTime = 0;
            HealthSystem.GameOver = true;
            //DO ENDGAME HERE
        }

        displayTime(endgameTime);
    }


    void displayTime(float display)
    {

        minutes = Mathf.FloorToInt(display / 60);
        seconds = Mathf.FloorToInt(display % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }



}
