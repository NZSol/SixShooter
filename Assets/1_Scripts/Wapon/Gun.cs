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
    [SerializeField] GameObject aimPoint;
    [SerializeField] Light muzzFlash;


    //ADS
    [SerializeField] TimeManager timeManager;
    bool aiming;
    bool canAim;
    float adsTimer;
    float adsBaseTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        gun = this.gameObject;
        timeToFire = 1.5f;
        adsTimer = 0;
        canAim = true;
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
            canAim = false;

            ammoCount--;
        }


        //Check Ammo and timer before firing again
        ShootTimer();
        CheckAmmo();
        if(canAim == false)
        {
            ADSCool();
        }
        
        //Declare if able to fire or not based upon timer and ammo
        if (ammoCount > 0 && timeToFire == 1.5f)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }
        

        //Reload Functions
        //if bool = true
        if (startReload == true)
        {
            StartCoroutine(Reload());
        }
        else
        {
            StopCoroutine(Reload());
        }
        //Manual
        if (ammoCount < 6 && Input.GetKeyDown(KeyCode.R))
        {
            ammoCount = 0;
            startReload = true;
        }

        print(canAim);
        print(canFire);
        print(adsTimer);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (canAim == true)
            {
                aiming = true;
            }
            else
            {
                aiming = false;
            }
        }

        if (aiming == true)
        {
            timeManager.DoSlowmo();
        }
        else
        {
            timeManager.ReduceSlowmo();
        }

    }



    //when ammo above 0, shoot ray, if hit target print hit and siplay blue line
    //If ammo below 0, start reload
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
                Debug.DrawRay(myCam.transform.position, myCam.transform.forward * 50, Color.red);
                print("missed");
            }
        }
        else
        {
            startReload = true;
        }
    }

    //Timer before shooting again
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

    //Ammo check
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

    //Countdown to reload over, assign values
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
    
    void ADSCool()
    {
        adsTimer -= (Time.deltaTime * 20);
        if(adsTimer <= 0)
        {
            adsTimer += adsBaseTime;
            canAim = true;
        }
    }




    //gameStart functions
    void AccesoryFunction()
    {
        aimPoint.transform.position = endPoint;
        endPoint = myCam.transform.forward * 50;
    }

}
