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
    bool reloading = false;
    bool startReload = false;

    //Ammo
    int ammoCount = 6;
    float timeToReload = 6;

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


        if (Input.GetKeyDown(KeyCode.Mouse0) && timeToFire >= 1.5f)
        {
            Shoot();
            timeToFire = 0;
            canFire = false;

            ammoCount--;
        }


        //Check Ammo and timer before firing again
        ShootTimer();
        CheckAmmo();

        //Declare if able to fire or not based upon timer and ammo
        if (ammoCount > 0 && timeToFire == 1.5f)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }
        


        if (startReload == true)
        {
            StartCoroutine(Reload());
        }
        else
        {
            StopCoroutine(Reload());
        }

        print(startReload);

        if (ammoCount < 6 && Input.GetKeyDown(KeyCode.R))
        {
            ammoCount = 0;
            startReload = true;
        }

    }

    void Shoot()
    {
        RaycastHit hit;
        if (ammoCount > 0)
        {
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
        else
        {
            startReload = true;
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


    IEnumerator Reload()
    {
        //animate reload
        yield return new WaitForEndOfFrame();
        timeToReload -= Time.deltaTime;
        

        if (timeToReload <= 0)
        {
            ammoCount = 6;
            timeToReload = 6;

        }
        if (ammoCount == 6 && timeToReload == 6)
        {
            startReload = false;
        }
    }
    






    void AccesoryFunction()
    {
        testOBJ.transform.position = endPoint;
        endPoint = myCam.transform.forward * 50;
    }

}
