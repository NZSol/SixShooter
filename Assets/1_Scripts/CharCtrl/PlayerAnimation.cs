using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            playerAnim.SetBool("Firing", true);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            playerAnim.SetBool("Firing", false);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            playerAnim.SetBool("Walking", true);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            playerAnim.SetBool("Walking", false);
        }




    }
}
