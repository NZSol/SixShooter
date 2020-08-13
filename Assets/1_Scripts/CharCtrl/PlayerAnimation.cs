using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator playerAnim;
    [SerializeField] string VertInput;
    [SerializeField] string HorizInput;
    [SerializeField] KeyCode shoot;
    [SerializeField] KeyCode ADS;
    [SerializeField] KeyCode Reload;
    CharacterController charControl;
    GameObject player;
    Gun gunScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        charControl = player.GetComponent<CharacterController>();
        gunScript = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(shoot) && !ButtonClick.isPaused && gunScript.canFire == true)
        {
            playerAnim.SetTrigger("Firing");
            
        }

        if (Input.GetAxis(VertInput)  != 0 && charControl.isGrounded || Input.GetAxis(HorizInput) != 0 && charControl.isGrounded)
        {
            playerAnim.SetBool("Walking", true);
        }
        else
        {
            playerAnim.SetBool("Walking", false);
        }

        float vertMove = Input.GetAxis(VertInput);

        if (Input.GetKey(ADS))
        {
            playerAnim.SetBool("ADS", true);
        }
        else
        {
            playerAnim.SetBool("ADS", false);
        }

        if (GetComponent<Gun>().startReload == true)
        {
           
            playerAnim.SetBool("Reloading", true);
            
        }
        else if (GetComponent<Gun>().startReload == false)
        {
            playerAnim.SetBool("Reloading", false);
        }
        //else if ()

        if (playerAnim.GetBool("Reloading") == true && Input.GetKeyDown(shoot) && gunScript.cancelReload == false)
        {
            gunScript.cancelReload = true;
            playerAnim.SetBool("Reloading", false);
        }

    }
}
