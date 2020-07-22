using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    float slowFactor = 0.05f;
    float slowDuration = 2f;

    public static bool endSlow;

    // Update is called once per frame
    void Update()
    {
        if(endSlow == true)
        {
            ReduceSlowmo();
        }
        if(Time.timeScale >= 1)
        {
            endSlow = false;
        }
    }

    public void DoSlowmo()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
        print("slowing");
    }

    public void ReduceSlowmo()
    {
        Time.timeScale += (1f / slowDuration) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
        print("speed");
    }
}
