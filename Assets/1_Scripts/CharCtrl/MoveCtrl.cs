using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    [SerializeField] string horizontalInputName;
    [SerializeField] string verticalInputName;
    [SerializeField] float moveSpeed;

    CharacterController charCtrl;

    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
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
}
