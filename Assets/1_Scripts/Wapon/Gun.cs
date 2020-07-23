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
    bool timeSwitch = false;
    Vector3 pos;
    Vector3 ADSPos;
    Vector3 scale;
    Vector3 ADSScale;
    Quaternion rot;
    Quaternion ADSRot;


    // Start is called before the first frame update
    void Start()
    {
        gun = this.gameObject;
        timeToFire = 1.5f;
        canAim = true;
        pos = gameObject.transform.position;
        rot = transform.rotation;
        scale = gameObject.transform.localScale;
        ADSPos = new Vector3(6.9f, -0.66f, 14.2f);
        ADSScale = new Vector3(25, 32, 32);
        ADSRot = Quaternion.Euler(-0.8f, 91, 2);

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
        ADSCheck();
        //if(canAim == false)
        //{
        //    ADSCool();
        //}
        
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

        print(canFire + "CanShoot");
        print(canAim + "CanAim");
        print(timeSwitch + "Switch");
        print(aiming + "Aiming");
    }



    //when ammo above 0, shoot ray, if hit target print hit and siplay blue line
    //If ammo below 0, start reload
    void Shoot()
    {
        RaycastHit hit;
        if (ammoCount > 0)
        {
            canAim = true;
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
    

    void ADSCheck()
    {
        if (Input.GetKey(KeyCode.Mouse1) && canAim == true)
        {
            aiming = true;
            lerpFuncIn();
        }
        else
        {
            aiming = false;
            timeSwitch = false;
            lerpFuncOut();
        }

        if (aiming == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            timeSwitch = !timeSwitch;
        }

        if(timeSwitch == true)
        {
            timeManager.DoSlowmo();
            print("Hitting Slow");
        }
        else
        {
            timeManager.ReduceSlowmo();
            print("hittingSpeed");
        }

    }

    void lerpFuncIn()
    {
        //lerpTimerFunc();
        //transform.position = Vector3.Lerp(pos, ADSPos, lerpTime);
        //transform.localScale = Vector3.Lerp(scale, ADSScale, lerpTime);
        //transform.localRotation =  Quaternion.Lerp(rot, ADSRot, lerpTime);
    }
    void lerpFuncOut()
    {
        //lerpTimerFunc();
        //transform.position = Vector3.Lerp(pos, ADSPos, lerpTime);
        //transform.localScale = Vector3.Lerp(scale, ADSScale, lerpTime);
        //transform.localRotation = Quaternion.Lerp(rot, ADSRot, lerpTime);
    }

    float lerpTime;
    void lerpTimerFunc()
    {
        Mathf.Clamp(lerpTime, 0, 1);

        if (aiming == true)
        {
            lerpTime += Time.deltaTime;
        }
        else
        {
            lerpTime -= Time.unscaledDeltaTime;
        }
    }


    //gameStart functions
    void AccesoryFunction()
    {
        aimPoint.transform.position = endPoint;
        endPoint = myCam.transform.forward * 50;
    }

}
