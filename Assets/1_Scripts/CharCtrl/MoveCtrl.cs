using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    //movement
    [SerializeField] string horizontalInputName;
    [SerializeField] string verticalInputName;
    [SerializeField] float moveSpeed;

    float crouchSpeed = 1.25f;
    float standSpeed = 2.5f;

    CharacterController charCtrl;


    //jumping
    bool isJumping;
    [SerializeField] private AnimationCurve jumpFalloff;
    [SerializeField] float jumpMultiplier;
    [SerializeField] KeyCode jumpKey;

    //Crouching
    bool ctrlToggle;

    [SerializeField] KeyCode crouchKey;
    Vector3 scaleAdjust, baseScale;
    bool scaleBool;
     

    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        scaleAdjust = new Vector3(transform.localScale.x, 1.5f, transform.localScale.z);
        baseScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        JumpInput();
        if (ctrlToggle == true)
        {
            CrouchHold();
        }
        else
        {
            CrouchToggle();
        }
        if (Input.GetKey(KeyCode.T))
        {
            ctrlToggle = !ctrlToggle;
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

    void CrouchToggle()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            scaleBool = !scaleBool;
        }

        if (scaleBool == true)
        {
            transform.localScale = scaleAdjust;
            moveSpeed = crouchSpeed;
        }
        else
        {
            transform.localScale = baseScale;
            moveSpeed = standSpeed;
        }
    }

    void CrouchHold()
    {
        if (Input.GetKey(crouchKey))
        {
            transform.localScale = scaleAdjust;
            moveSpeed = crouchSpeed;
        }
        else
        {
            transform.localScale = baseScale;
            moveSpeed = standSpeed;
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
