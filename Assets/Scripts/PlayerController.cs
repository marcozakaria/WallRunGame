using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform centerTansform;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float downForceMul = 0.7f;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Rigidbody playerRG;
    private bool isGrounded , doubleJump;

    private void Update()
    {
        if (GameManager.instance.isPlaying)
        {
           // playerTransform.RotateAround(centerTansform.position, Vector3.up, Time.deltaTime * speed);

            if (Input.GetMouseButtonDown(0))
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
       // Quaternion angleQ = Quaternion.AngleAxis(Time.fixedDeltaTime * speed, Vector3.up);
        //playerRG.MoveRotation(angleQ * playerRG.rotation);
        //playerRG.MovePosition(angleQ * (playerTransform.position - centerTansform.position) + centerTansform.position);

        if (Physics.Raycast(playerTransform.position, -Vector3.up, 1, groundLayer))
        {
            isGrounded = doubleJump =true;
        }
        else
        {
            isGrounded = false;
            if (playerRG.velocity.y < 0)
            {
                playerRG.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * downForceMul;
            }
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            playerRG.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (doubleJump)
        {
            doubleJump = false;
            playerRG.AddForce(Vector3.up * jumpForce/2, ForceMode.Impulse);
        }       
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
