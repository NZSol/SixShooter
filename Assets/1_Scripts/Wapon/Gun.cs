using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //raycast Variables
    [SerializeField] float range = 50;
    GameObject gun;
    [SerializeField] Camera myCam;
    Vector3 endPoint;
    Ray ray;

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
    [SerializeField] GameObject muzzFlash;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem bloodParticle;


    //ADS
    [SerializeField] TimeManager timeManager;
    bool aiming;
    bool canAim;
    bool timeSwitch = false;

    [SerializeField] Transform GunPosBase;
    [SerializeField] Transform gunPosADS;

    // Start is called before the first frame update
    void Start()
    {
        gun = this.gameObject;
        timeToFire = 1.5f;
        canAim = true;
        muzzFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AccesoryFunction();
        muzzFlash.SetActive(false);

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

    }


    //when ammo above 0, shoot ray, if hit target print hit and siplay blue line
    //If ammo below 0, start reload
    void Shoot()
    {
        RaycastHit hit;
        if (ammoCount > 0)
        {
            StartCoroutine(MuzzleFlash());
            if (Physics.Raycast(ray, out hit, range, 1 << 10))
            {
                Debug.DrawRay(myCam.transform.position, myCam.transform.forward * 50, Color.green);
                print("hit" + hit.transform.name);
                Instantiate(bloodParticle, hit.point, transform.rotation);
                
                if(hit.transform.tag == "critpint")
                {
                    hit.transform.GetComponent<AIBase>().CritDamage(5);
                }
                else
                {
                    hit.transform.GetComponent<AIBase>().RegDamage(3);
                }
            }
            else if (Physics.Raycast(ray, out hit, range))
            {
                Debug.DrawRay(myCam.transform.position, myCam.transform.forward * 50, Color.yellow);
                print("missed");
                Instantiate(hitParticle, hit.point, transform.rotation);
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

    IEnumerator MuzzleFlash()
    {
        muzzFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzFlash.SetActive(false);
        StopCoroutine(MuzzleFlash());
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
        canAim = false;

        if (timeToReload <= 0)
        {
            ammoCount = 6;
            timeToReload = 6;
            canAim = true;
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

        if (aiming == true && Input.GetKeyDown(KeyCode.LeftShift) && lerpTime >= 1)
        {
            timeSwitch = !timeSwitch;
        }

        if(timeSwitch == true)
        {
            timeManager.DoSlowmo();
        }
        else
        {
            timeManager.ReduceSlowmo();
        }

    }

    float FOVHip = 60;
    float FOVAim = 50;
    float camFOV;

    void lerpFuncIn()
    {
        lerpTimerFunc();
        transform.position = Vector3.Lerp(GunPosBase.position, gunPosADS.position, lerpTime);
        transform.localScale = Vector3.Lerp(GunPosBase.localScale, gunPosADS.localScale, lerpTime);
        transform.localRotation = Quaternion.Lerp(GunPosBase.localRotation, gunPosADS.localRotation, lerpTime);
        camFOV = Mathf.Lerp(FOVHip, FOVAim, lerpTime);
    }
    void lerpFuncOut()
    {
        lerpTimerFunc();
        transform.position = Vector3.Lerp(GunPosBase.position, gunPosADS.position, lerpTime);
        transform.localScale = Vector3.Lerp(GunPosBase.localScale, gunPosADS.localScale, lerpTime);
        transform.localRotation = Quaternion.Lerp(GunPosBase.localRotation, gunPosADS.localRotation, lerpTime);
        camFOV = Mathf.Lerp(FOVHip, FOVAim, lerpTime);
    }

    float lerpTime;
    [SerializeField] float multiplier;
    void lerpTimerFunc()
    {
        lerpTime = Mathf.Clamp(lerpTime, 0, 1);

        if (aiming == true && lerpTime < 1)
        {
            lerpTime += Time.deltaTime * multiplier;
        }
        else if (aiming == false && lerpTime > 0)
        {
            lerpTime -= Time.unscaledDeltaTime * multiplier;
        }
    }


    //gameStart functions
    void AccesoryFunction()
    {
        endPoint = myCam.transform.forward * 50;
        myCam.fieldOfView = camFOV;
        ray = myCam.ViewportPointToRay(new Vector2(0.5f, 0.5f));
    }

}
