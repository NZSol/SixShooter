using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    public Image endPrompt;
    public bool fadeIn = false;
    // Start is called before the first frame update
    void Start()
    {
        endPrompt.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn == false)
        {
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EndPrompt")
        {
            StartCoroutine(FadeImageOut(true));
        }
    }
    IEnumerator FadeImageOut(bool fadeAway)
    {
        if (fadeAway)
        {
            for(float i = 1; i >= 0; i+= Time.deltaTime)
            {
                endPrompt.color = new Color(1, 1, 1,i);
                yield return null;
            }
        }

    }
}
