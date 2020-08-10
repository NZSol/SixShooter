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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(shoot) && !ButtonClick.isPaused)
        {
            playerAnim.SetBool("Firing", true);
            
        }

        if (Input.GetAxis(VertInput) != 0 || Input.GetAxis(HorizInput) != 0)
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
            playerAnim.SetBool("Firing", false);
        }
        else
        {
            playerAnim.SetBool("Reloading", false);
        }

        if (playerAnim.GetBool("Reloading") == true && Input.GetKeyDown(shoot))
        {
            playerAnim.SetBool("Reloading", false);
        }

    }
}
