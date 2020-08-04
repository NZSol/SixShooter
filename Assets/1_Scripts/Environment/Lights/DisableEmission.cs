using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEmission : MonoBehaviour
{
    Material mat;
    Renderer render;


    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        mat = render.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<LightFlicker>().lightEnable == true)
        {
            mat.DisableKeyword("_EMISSION");
        }
        else
        {
            mat.EnableKeyword("_EMISSION");
        }
    }
}
