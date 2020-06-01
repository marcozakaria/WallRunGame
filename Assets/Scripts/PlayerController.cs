using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed = 50;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float downForceMul = 0.7f;
    [SerializeField] LayerMask groundLayer;

    [Header("components")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform centerTansform;
    [SerializeField] Rigidbody playerRG;
    [SerializeField] Animator animator;

    private bool isGrounded, doubleJump;

    public void StartGame()
    {
        animator.SetBool("move", true);
    }

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
        /*  Quaternion angleQ = Quaternion.AngleAxis(Time.fixedDeltaTime * speed, centerTansform.up);
          playerRG.MovePosition(angleQ * (playerTransform.position - centerTansform.position) + centerTansform.position);
          playerRG.MoveRotation(angleQ * playerTransform.rotation);*/
        if (GameManager.instance.isPlaying)
        {
            transform.RotateAround(centerTansform.position, transform.up, Time.deltaTime * -speed);

            if (Physics.Raycast(playerTransform.position, -Vector3.up, 1, groundLayer))
            {
                isGrounded = doubleJump = true;
                animator.SetBool("isGrounded", true);
            }
            else
            {
                isGrounded = false;
                if (playerRG.velocity.y < 0)
                {
                    playerRG.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * downForceMul;
                }
                else
                {
                    Vector3 vel = playerRG.velocity;
                    vel.y -= 9.8f * Time.deltaTime;
                    playerRG.velocity = vel;
                }
            }
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            AudioManager.instance.Play("jump");
            animator.SetTrigger("jump");
            playerRG.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (doubleJump)
        {
            AudioManager.instance.Play("jump");
            animator.SetTrigger("jump");
            doubleJump = false;
            playerRG.AddForce(Vector3.up * (playerRG.velocity.y < 0 ? jumpForce : jumpForce/2f), ForceMode.Impulse);
        }       
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("death"))
        {
            MyObjectPool.instance.SpawnParticleFromPool(2, transform.position);
            GameManager.instance.OnGameOver();
            gameObject.SetActive(false);
        }
    }*/
}
