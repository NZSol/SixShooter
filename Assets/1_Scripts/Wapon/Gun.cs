using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class Gun : MonoBehaviour
{
    //raycast Variables
    [SerializeField] float range = 50;
    GameObject gun;
    [SerializeField] Camera myCam;
    Vector3 endPoint;
    Ray ray;
    private int bulletsFired;

    //Fire Timer
    float timeToFire;
    public bool canFire = false;
    bool reloading = false;
    public bool startReload = false;
    public AudioSource recharging;

    //Ammo
    public int ammoCount = 6;
    float timeToReload = 6;
    public bool cancelReload;

    //Other
    [SerializeField] GameObject muzzFlash;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem muzzleflash;
    [SerializeField] ParticleSystem bloodParticle;
    [SerializeField] ParticleSystem barrelExplosion;
    [SerializeField] ParticleSystem critEffect;
    [SerializeField] ParticleSystem RegEffect;
    [SerializeField] float speedModifier;
    [SerializeField] GameObject gunMesh;

    AudioManager audioManager;
    


    //ADS
    [SerializeField] TimeManager timeManager;
    bool aiming;
    bool canAim;
    bool canSlow;
    public bool timeSwitch = false;
    public AudioSource slowSfx;

    //Canvas
    [SerializeField] Text ammoCountTxt;
    [SerializeField] Image ammoCountImg6;
    [SerializeField] Image ammoCountImg5;
    [SerializeField] Image ammoCountImg4;
    [SerializeField] Image ammoCountImg3;
    [SerializeField] Image ammoCountImg2;
    [SerializeField] Image ammoCountImg1;
    [SerializeField] Image crossHair;


    //AnimatorStuff
    Animator anim;
    [SerializeField] AnimationClip shootClip;

    // Start is called before the first frame update
    void Start()
    {
        bulletsFired = 0;
        gun = this.gameObject;
        canAim = true;
        canSlow = true;
        muzzFlash.SetActive(false);
        Renderer render = gunMesh.gameObject.GetComponent<Renderer>();
        mat = render.material;
        mat.SetColor("_EmissionColor", Color.white * intenseMax);
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        Cursor.lockState = CursorLockMode.Locked;

        ammoCountImg6.enabled = true;
        ammoCountImg5.enabled = true;
        ammoCountImg4.enabled = true;
        ammoCountImg3.enabled = true;
        ammoCountImg2.enabled = true;
        ammoCountImg1.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        AccesoryFunction();

        if (aiming == true)
        {
            MoveCtrl.aimSpeedModif = speedModifier;
        }
        else
        {
            MoveCtrl.aimSpeedModif = 1;
        }
        

        //Check Ammo before firing again
        CheckAmmo();
        ADSCheck();
        ADSCool();
        


        //Reload Functions
        //if bool = true
        if (startReload == true)
        {
            StartCoroutine(Reload());
            canFire = false;
        }
        else
        {
            StopCoroutine(Reload());
        }
        //Manual
        if (ammoCount < 6 && Input.GetKeyDown(KeyCode.R))
        {
            startReload = true;
        }

    }

    public void GunShoot()
    {
        timeToFire = 0;
        ammoCount--;
        canFire = false;
        muzzFlash.SetActive(true);
        StartCoroutine(MuzzleFlashOff());
        muzzleflash.Play();
        bulletsFired++;
        Shoot();

        if (timeSwitch == true)
        {
            slowTimer = 0;
            canSlow = false;
        }
        timeSwitch = false;
    }

    public GameObject hitObj;
    //when ammo above 0, shoot ray, if hit target, spawn particle effect on hit pos
    //If ammo below 0, start reload
    void Shoot()
    {
        RaycastHit hit;
        if (ammoCount > 0)
        {
            if (Physics.Raycast(ray, out hit, range, 1 << 10))
            {
                Debug.DrawRay(myCam.transform.position, myCam.transform.forward * 50, Color.green);
                hitObj = hit.collider.gameObject;
                if (hit.collider.tag == "regDamage")
                {
                    //Instantiate(RegEffect, hit.point, transform.rotation);
                    //Play SoundEffect
                }
                else if (hit.collider.tag == "critPoint")
                {
                    Instantiate(critEffect, hit.point, transform.rotation);
                    audioManager.Play("CritHit");
                }

                Instantiate(bloodParticle, hit.point, transform.rotation);

                runDamageMethod();
            }
            else if (Physics.Raycast(ray, out hit, range))
            {
                Debug.DrawRay(myCam.transform.position, myCam.transform.forward * 50, Color.yellow);
                Debug.Log("HitParticle");
                Instantiate(hitParticle, hit.point, transform.rotation);
                if (hit.collider.tag == "Destructible")
                {
                    Destroy(hit.transform.gameObject);
                    hit.transform.gameObject.GetComponent<Spawning>().EmptyList();
                    print("destoryObj");
                }
                if (hit.collider.tag == "Barrel")
                {
                    Destroy(hit.transform.gameObject);
                    Instantiate(barrelExplosion, hit.point, transform.rotation);
                    audioManager.Play("BarrelExplosion");
                    print("destoryObj");
                }
                else if (hit.collider.tag == "extendBridge" )
                {
                    print("Bridge");
                    Animator anim = hit.transform.gameObject.GetComponentInChildren<Animator>();
                    anim.SetBool("Extend", true);
                }
            }
            else
            {
                Debug.DrawRay(myCam.transform.position, myCam.transform.forward * 50, Color.red);
                print("missedShot");
            }
        }
        else
        {
            startReload = true;
        }
    }


        IEnumerator MuzzleFlashOff()
        {
            yield return new WaitForSeconds(0.15f);
            muzzFlash.SetActive(false);
        }

    void runDamageMethod()
    {
        if(hitObj.tag == "critPoint")
        {
            hitObj.GetComponentInParent<AIBase>().Damage(i: 5);
            print("hitCrit");
        }
        else if(hitObj.tag == "regDamage")
        {
            hitObj.GetComponentInParent<AIBase>().Damage(i: 3);
            print("hitBase");
        }
    }

    public void RecoilFinish()
    {
        anim.SetBool("Firing", false);
        anim.SetBool("ExitTime", true);
        if (ammoCount > 0)
        {
            canFire = true;
            StartCoroutine(CheckFire());
        }

    }
    IEnumerator CheckFire()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Firing", false);
    }
    public void Unlock()
    {
        canFire = true;
        //print("hit");
    }

    public void ReloadBullet()
    {
        if (cancelReload == false)
        {
            if (ammoCount <= 5)
            {
                ammoCount++;
                StartCoroutine(enableShoot());
            }
            if (ammoCount == 6)
            {
                startReload = false;
                anim.SetBool("ExitTime", false);
                anim.Play("Transition_from_Reload");
                canAim = true;
            }
        }
        else
        {
            cancelReload = false;
            canFire = true;
            canAim = true;
            startReload = false;
            anim.Play("Transition_from_Reload");
            anim.SetBool("ExitTime", false);
            ammoCount++;
        }

    }

    public void TransitionFromReload()
    {
        if (cancelReload == true)
        {
            canFire = true;
            canAim = true;
            startReload = false;
            cancelReload = false;

        }
    }

    IEnumerator enableShoot()
    {
        yield return new WaitForSeconds(0.1f);
        canFire = true;
        canAim = true;
        StopCoroutine(enableShoot());
    }

    //Ammo check
    void CheckAmmo()
    {
        if (ammoCount <= 0)
        {
            canFire = false;
            ammoCount = 0;
            ammoCountImg6.enabled = false;
            ammoCountImg5.enabled = false;
            ammoCountImg4.enabled = false;
            ammoCountImg3.enabled = false;
            ammoCountImg2.enabled = false;
            ammoCountImg1.enabled = false;
        }

        if(ammoCount == 6)
        {
            ammoCountImg6.enabled = true; 
            ammoCountImg5.enabled = true;
            ammoCountImg4.enabled = true;
            ammoCountImg3.enabled = true;
            ammoCountImg2.enabled = true;
            ammoCountImg1.enabled = true;
        }

        if (ammoCount == 5)
        {
            ammoCountImg6.enabled = true;
            ammoCountImg5.enabled = true;
            ammoCountImg4.enabled = true;
            ammoCountImg3.enabled = true;
            ammoCountImg2.enabled = true;
            ammoCountImg1.enabled = false;
        }

        if (ammoCount == 4)
        {
            ammoCountImg6.enabled = true;
            ammoCountImg5.enabled = true;
            ammoCountImg4.enabled = true;
            ammoCountImg3.enabled = true;
            ammoCountImg2.enabled = false;
            ammoCountImg1.enabled = false;
        }

        if (ammoCount == 3)
        {
            ammoCountImg6.enabled = true;
            ammoCountImg5.enabled = true;
            ammoCountImg4.enabled = true;
            ammoCountImg3.enabled = false;
            ammoCountImg2.enabled = false;
            ammoCountImg1.enabled = false;
        }

        if (ammoCount == 2)
        {
            ammoCountImg6.enabled = true;
            ammoCountImg5.enabled = true;
            ammoCountImg4.enabled = false;
            ammoCountImg3.enabled = false;
            ammoCountImg2.enabled = false;
            ammoCountImg1.enabled = false;
        }
        if (ammoCount == 1)
        {
            ammoCountImg6.enabled = true;
            ammoCountImg5.enabled = false;
            ammoCountImg4.enabled = false;
            ammoCountImg3.enabled = false;
            ammoCountImg2.enabled = false;
            ammoCountImg1.enabled = false;
        }

        //ammoCountTxt.text = ammoCount + "/6";
    }

    //Countdown to reload over, assign values
    IEnumerator Reload()
    {
        //animate reload
        yield return new WaitForEndOfFrame();
        timeToReload -= Time.deltaTime;
        //canAim = false;
        //canFire = false;


    }


    [SerializeField] Material mat;
    float intensity;
    float intenseMin = 0.1f;
    float intenseMax = 5f;

    void ADSCheck()
    {
        if (Input.GetKey(KeyCode.Mouse1) && canAim == true)
        {
            crossHair.enabled = false;
            aiming = true;
            lerpFuncIn();
        }
        else
        {
            aiming = false;
            crossHair.enabled = true;
            timeSwitch = false;
            lerpFuncOut();
        }

        if (aiming == true && Input.GetKeyDown(KeyCode.LeftShift) && lerpTime >= 1 && canSlow == true)
        {
            timeSwitch = true;
            slowSfx.Play();
        }
        else if (canSlow == false)
        {
            timeSwitch = false;
        }

        if(timeSwitch == true)
        {
            timeManager.DoSlowmo();
            hiltBrightnessLerp();
        }
        else if (timeSwitch == false)
        {
            hiltBrightnessLerp();
            timeManager.ReduceSlowmo();
        }

        //intensity
        if (canSlow == false)
        {
            intensity = Mathf.Lerp(intenseMin, intenseMax, slowTimer);
        }
        else if (canSlow == true)
        {
            intensity = Mathf.Lerp(intenseMin, intenseMax, slowTimer);
        }
    }

    float FOVHip = 60;
    float FOVAim = 50;
    float camFOV;

    void lerpFuncIn()
    {
        lerpTimerFunc();
        //transform.position = Vector3.Lerp(GunPosBase.position, gunPosADS.position, lerpTime);
        //transform.localScale = Vector3.Lerp(GunPosBase.localScale, gunPosADS.localScale, lerpTime);
        //transform.localRotation = Quaternion.Lerp(GunPosBase.localRotation, gunPosADS.localRotation, lerpTime);
        camFOV = Mathf.Lerp(FOVHip, FOVAim, lerpTime);
    }
    void lerpFuncOut()
    {
        lerpTimerFunc();
        //transform.position = Vector3.Lerp(GunPosBase.position, gunPosADS.position, lerpTime);
        //transform.localScale = Vector3.Lerp(GunPosBase.localScale, gunPosADS.localScale, lerpTime);
        //transform.localRotation = Quaternion.Lerp(GunPosBase.localRotation, gunPosADS.localRotation, lerpTime);
        camFOV = Mathf.Lerp(FOVHip, FOVAim, lerpTime);
    }

    float hiltLerpTimer;
    void hiltBrightnessLerp()
    {
        hiltLerpTimer = Mathf.Clamp(hiltLerpTimer, 0, 1);
        if (timeSwitch == true && hiltLerpTimer < 1)
        {
            hiltLerpTimer += Time.unscaledDeltaTime / 2;
        }
        else if (timeSwitch == false && hiltLerpTimer > 0)
        {
            hiltLerpTimer -= Time.unscaledDeltaTime * 4;
        }
    }

    public float lerpTime;
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

    float slowTimer;
    void ADSCool()
    {
        slowTimer = Mathf.Clamp(slowTimer, 0, 1);
        //reduce float time when zoomed and can slow time
        if (timeSwitch == true && aiming == true)
        {
            slowTimer -= (Time.unscaledDeltaTime / 3);

        }
        //increase timer when not slowTime
        else if (timeSwitch == false)
        {
            slowTimer += (Time.deltaTime / 6);
        }


        if (slowTimer <= 0)
        {
            recharging.Play();
            canSlow = false;
        }
        else if (slowTimer > 0)
        {
            canSlow = true;
        }
    }


    //gameStart functions
    void AccesoryFunction()
    {
        endPoint = myCam.transform.forward * 50;
        myCam.fieldOfView = camFOV;
        ray = myCam.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        mat.SetColor("_EmissionColor", Color.white * intensity);
    }






    void testing()
    {
    }

    private void OnApplicationQuit()
    {
#if !UNITY_EDITOR

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Game", bulletsFired);

#endif
    }



}
