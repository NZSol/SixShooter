using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    //movement
    [SerializeField] string horizontalInputName;
    [SerializeField] string verticalInputName;
    float moveSpeed;
    Vector3 moveState;

    CharacterController charCtrl;

    PlayerSoundEvents playerSoundScript;
    Animator playerAnimator;


    //jumping
    bool isJumping;
    [SerializeField] private AnimationCurve jumpFalloff;
    [SerializeField] float jumpMultiplier;
    [SerializeField] KeyCode jumpKey;

    //crouching
    public static bool toggleCrouchMode = true;
    [SerializeField] KeyCode crouchKey;
    bool crouching;
    [SerializeField] GameObject viewManager;
    [SerializeField] float crouchSpeed = 1.25f;
    [SerializeField] float standSpeed = 2.5f;
    public static float aimSpeedModif = 1;
    [SerializeField] float height;
    float heightY;
    [SerializeField] float crouchOffset;

    float offset;

    //timeSlowSpeed
    [SerializeField] AnimationCurve slowFallOff;
    float slowTimeMultip;
    float falloff;
    [SerializeField] float multiplier;

    //Ladder Stuff
    bool ladderBool = false;
    [SerializeField] Camera cam;
    public float range = 1;
    public float rangeFromLadder;
    public float climbSpeed;
    public GameObject ladder;
    Vector3 rayDir;
    float yPos;

    public enum States{MoveState, LadderState}
    public States stateEnum;

    
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        heightY = viewManager.transform.position.y;
        offset = viewManager.transform.position.y - transform.position.y;
        timeSlowBase = 1;
        slowTimeMultip = slowFallOff.Evaluate(falloff);
        stateEnum = States.MoveState;
        Cursor.lockState = CursorLockMode.Confined;
        playerSoundScript = GetComponentInChildren<PlayerSoundEvents>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, cam.transform.forward * range, Color.magenta);
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit, range, 1 << 12) && charCtrl.isGrounded)
        {
            ladder = hit.transform.gameObject;
            stateEnum = States.LadderState;
        }

        switch (stateEnum)
        {
            case States.MoveState:
                PlayerMove();
                if (ladderBool == true)
                {
                    ladderBool = false;
                }
                break;

            case States.LadderState:
                ladderMove(yPos);
                if(ladderBool == false)
                {
                    yPos = transform.position.y;
                    ladderBool = true;
                }
                break;
        }
        if (ladder != null)
        {
            rayDir = ladder.transform.position - this.gameObject.transform.position;
        }
    }

    void ladderMove(float i)
    {
        //float playerDist = Vector3.Distance(transform.position, ladder.transform.position);
        RaycastHit hit;
        if(transform.position.y < i)
        {
            stateEnum = States.MoveState;
        }
        else
        {
            if (Physics.Raycast(transform.position, rayDir, out hit, rangeFromLadder, 1 << 12))
            {
                Debug.DrawRay(transform.position, rayDir * rangeFromLadder, Color.white);
                float vertInput = Input.GetAxis(verticalInputName) * climbSpeed;
                Vector3 climb = transform.up * vertInput;
                moveState = climb * Time.deltaTime;
                transform.position += moveState;

                print(climb);

            }
            else
            {
                Debug.DrawRay(transform.position, rayDir * rangeFromLadder, Color.red);
                stateEnum = States.MoveState;
            }
        }
    }

    void PlayerMove()
    {
        ladder = null;
        heightY = transform.position.y + offset;
        //timeSlowMult = Mathf.Lerp(timeSlowMin, timeSlowBase, Time.timeScale);
        if (GetComponentInChildren<Gun>().timeSwitch == true && Time.timeScale != 1)
        {
            timeSlowMult = slowTimeMultip * (Time.timeScale * multiplier);
        }
        else timeSlowMult = 1;
        if (Input.GetKeyDown(KeyCode.T))
        {
            toggleCrouchMode = !toggleCrouchMode;
        }
        if (toggleCrouchMode == true)
        {
            CrouchTogg();
        }
        else
        {
            CrouchHold();
        }

        playerMove();
    }

    void CrouchTogg()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            crouching = !crouching;
        }

        if (crouching == true)
        {
            charCtrl.height = height;
            charCtrl.center = new Vector3(0, -0.25f, 0);
            viewManager.transform.position = new Vector3 (transform.position.x, heightY - crouchOffset, transform.position.z);
            moveSpeed = crouchSpeed;
        }
        else
        {
            charCtrl.height = 2;
            charCtrl.center = Vector3.zero;
            viewManager.transform.position = new Vector3(transform.position.x, heightY, transform.position.z);
            moveSpeed = standSpeed;
        }
    }
    void CrouchHold()
    {
        if (Input.GetKey(crouchKey))
        {
            charCtrl.height = height;
            charCtrl.center = new Vector3(0, -0.25f, 0);
            viewManager.transform.position = new Vector3(transform.position.x, heightY - crouchOffset, transform.position.z);
            moveSpeed = crouchSpeed;
        }
        else
        {
            charCtrl.height = 2;
            charCtrl.center = Vector3.zero;
            viewManager.transform.position = new Vector3(transform.position.x, heightY, transform.position.z);;
            moveSpeed = standSpeed;
        }
    }

    public static Vector3 forwardMovement, rightMovement;
    [SerializeField] float timeSlowMin;
    float timeSlowBase, timeSlowMult;
    public float angularMultiplier;

    void playerMove()
    {
        float vertInput = Input.GetAxis(verticalInputName) * moveSpeed;
        float horizInput = Input.GetAxis(horizontalInputName) * moveSpeed;

        forwardMovement = transform.forward * vertInput;
        rightMovement = transform.right * horizInput;

        moveState = new Vector3((rightMovement.x + forwardMovement.x) * (aimSpeedModif), (rightMovement.y + forwardMovement.y) * (aimSpeedModif), (rightMovement.z + forwardMovement.z) * (aimSpeedModif));

        if(Input.GetButton(verticalInputName) && Input.GetButton(horizontalInputName))
        {
            Vector3 angularRgtMove = rightMovement/2;
            Vector3 angularFwdMove = forwardMovement / 2;
            moveState = new Vector3((angularFwdMove.x + angularRgtMove.x) * aimSpeedModif, (angularFwdMove.y + angularRgtMove.y) * aimSpeedModif, (angularFwdMove.z + angularRgtMove.z) * aimSpeedModif) * angularMultiplier;
        }

        if (isJumping == false && !Input.GetKeyDown(jumpKey))
        {
            charCtrl.SimpleMove(moveState * timeSlowMult);
            
        }
        else
        {
            charCtrl.SimpleMove(Vector3.zero);
        }

        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            
            playerAnimator.SetBool("Jumping", true);
            isJumping = true;
            playerSoundScript.JumpSound();
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        charCtrl.slopeLimit = 90;
        float timeInAir = 0;

        do
        {
            float jumpForce = jumpFalloff.Evaluate(timeInAir);
            charCtrl.Move(((Vector3.up * jumpMultiplier) * jumpForce * Time.deltaTime) + (moveState * Time.deltaTime));
            playerAnimator.SetBool("Walking", false);
            timeInAir += Time.deltaTime;

            yield return null;
        }
        while (!charCtrl.isGrounded && charCtrl.collisionFlags != CollisionFlags.Above);

        

        playerAnimator.SetBool("Walking", true);
        playerAnimator.SetBool("Jumping", false);
        playerSoundScript.LandingSound();
        isJumping = false;
        charCtrl.slopeLimit = 45;
    }
}
