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

    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        heightY = viewManager.transform.position.y;
        offset = viewManager.transform.position.y - transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        heightY = transform.position.y + offset;

        playerMove();

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

    void playerMove()
    {
        float vertInput = Input.GetAxis(verticalInputName) * moveSpeed;
        float horizInput = Input.GetAxis(horizontalInputName) * moveSpeed;

        forwardMovement = transform.forward * vertInput;
        rightMovement = transform.right * horizInput;

        moveState = new Vector3((rightMovement.x + forwardMovement.x) * (aimSpeedModif), (rightMovement.y + forwardMovement.y) * (aimSpeedModif), (rightMovement.z + forwardMovement.z) * (aimSpeedModif));

        if (isJumping == false && !Input.GetKeyDown(jumpKey))
        {
            charCtrl.SimpleMove(moveState);
        }
        else
        {
            charCtrl.SimpleMove(Vector3.zero);
        }

        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
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
            timeInAir += Time.deltaTime;

            yield return null;
        }
        while (!charCtrl.isGrounded && charCtrl.collisionFlags != CollisionFlags.Above);

        isJumping = false;
        charCtrl.slopeLimit = 45;
    }
}
