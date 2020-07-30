using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlash : MonoBehaviour
{

    Material mat;
    Renderer render;
    public bool check;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        mat = render.material;
        mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
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
