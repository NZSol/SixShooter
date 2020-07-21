using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    //movement
    [SerializeField] string horizontalInputName;
    [SerializeField] string verticalInputName;
    [SerializeField] float moveSpeed;

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
    Vector3 baseScale, adjustScale;
    [SerializeField] GameObject viewManager;
    float crouchSpeed = 1.25f;
    float standSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        baseScale = transform.localScale;
        adjustScale = new Vector3(transform.localScale.x, 1.5f, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        print(toggleCrouchMode);
        playerMove();
        JumpInput();

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
            charCtrl.height = 1.5f;
            charCtrl.center = new Vector3(0, -0.25f, 0);
            viewManager.transform.position = new Vector3 (transform.position.x, transform.position.y - 0.4f, transform.position.z);
            moveSpeed = crouchSpeed;
        }
        else
        {
            charCtrl.height = 2;
            charCtrl.center = Vector3.zero;
            viewManager.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            moveSpeed = standSpeed;
        }
    }
    void CrouchHold()
    {
        if (Input.GetKey(crouchKey))
        {
            charCtrl.height = 1.5f;
            charCtrl.center = new Vector3(0, -0.25f, 0);
            viewManager.transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
            moveSpeed = crouchSpeed;
        }
        else
        {
            charCtrl.height = 2;
            charCtrl.center = Vector3.zero;
            viewManager.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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

        charCtrl.SimpleMove((rightMovement + forwardMovement) * moveSpeed);

    }

    void JumpInput()
    {
        if (Input.GetKey(jumpKey) && !isJumping)
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
            charCtrl.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;

            yield return null;
        }
        while (!charCtrl.isGrounded && charCtrl.collisionFlags != CollisionFlags.Above);

        isJumping = false;
        charCtrl.slopeLimit = 45;
    }
}
