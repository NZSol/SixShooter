using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //raycast Variables
    float range = 50;
    GameObject gun;
    [SerializeField] Camera myCam;
    Vector3 endPoint;

    //Fire Timer
    float timeToFire;
    bool canFire = true;

    //Ammo
    int ammoCount = 6;


    //Other
    [SerializeField] GameObject testOBJ;





    // Start is called before the first frame update
    void Start()
    {
        gun = this.gameObject;
        timeToFire = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        AccesoryFunction();


        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire == true)
        {
            Shoot();
            timeToFire = 0;
            canFire = false;

            ammoCount--;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && ammoCount <= 0)
        {
            Reload();
        }

        ShootTimer();

        print(canFire);


        CheckAmmo();



    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(myCam.transform.position, endPoint, out hit, range, 1 << 9))
        {
            Debug.DrawRay(myCam.transform.position, myCam.transform.forward * 50, Color.blue);
            print("hit");
        }
        else
        {
            print("missed");
        }
    }


    void ShootTimer()
    {
        if (timeToFire < 1.5f)
        {
            timeToFire += Time.deltaTime;
        }
        else
        {
            timeToFire = 1.5f;
            canFire = true;
        }
    }


    void CheckAmmo()
    {
        if (ammoCount <= 0)
        {
            canFire = false;
        }
        else
        {
            canFire = true;
        }
    }

    void Reload()
    {
        //animate reload

    }








    void AccesoryFunction()
    {
        testOBJ.transform.position = endPoint;
        endPoint = myCam.transform.forward * 50;
    }

}
