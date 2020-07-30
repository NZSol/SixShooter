using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlash : MonoBehaviour
{

    Material mat;
    Renderer render;
    public bool check;
    bool triggered;
    [SerializeField] float flickerMin;
    [SerializeField] float flickerMax;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        mat = render.material;
        mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            LightEnable();
        }
        else
        {
            LightDisable();
        }

        if (triggered == false)
        {
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            check = !check;
            yield return new WaitForSeconds(Random.Range(flickerMin, flickerMax));
        }
    }

    void LightEnable()
    {
        mat.EnableKeyword("_EMISSION");
    }

    void LightDisable()
    {
        mat.DisableKeyword("_EMISSION");
    }

}
