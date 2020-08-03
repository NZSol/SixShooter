using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light thisLight;
    public float minFlickerSpeed = 0.1f;
    public float maxFlickerSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lightFlickering());
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    IEnumerator lightFlickering()
    {
        while (true)
        {
            thisLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
            thisLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        }


    }
}
