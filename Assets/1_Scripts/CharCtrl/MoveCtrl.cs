using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    [SerializeField] string horizontalInputName;
    [SerializeField] string verticalInputName;
    [SerializeField] float moveSpeed;

    CharacterController charCtrl;

    bool isJumping;
    [SerializeField] private AnimationCurve jumpFalloff;
    [SerializeField] float jumpMultiplier;
    [SerializeField] KeyCode jumpKey;
     

    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        JumpInput();
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
