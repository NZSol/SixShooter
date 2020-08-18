using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{

    float slowFactor = 0.05f;
    float slowDuration = 2f;

    public static bool endSlow;
    public static bool GamePause;

    // Update is called once per frame
    private void Start()
    {

    }
    void Update()
    {
        if (GamePause == false)
        {
            Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = false;
            if (endSlow == true)
            {
                ReduceSlowmo();
            }
            if (Time.timeScale >= 1)
            {
                endSlow = false;
            }
        }
        else
        {
            PauseMenu();
        }
    }

    IEnumerator SlowMoHaptics()
    {
        Gamepad.current.SetMotorSpeeds(.1f, .1f);
        yield return new WaitForSeconds(3);
        InputSystem.ResetHaptics();
    }

    public void DoSlowmo()
    {
        Time.timeScale = slowFactor;
        StartCoroutine(SlowMoHaptics());
        //Time.fixedDeltaTime = Time.timeScale * 0.2f;
    }

    public void ReduceSlowmo()
    {
        Time.timeScale += (1f / slowDuration) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
    }

    public static void PauseMenu()
    {

        Time.timeScale = 0;
        //Time.fixedDeltaTime = Time.timeScale * 0.005f;
    }
}