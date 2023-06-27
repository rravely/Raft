using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerState playerState;
    Rigidbody playerRigid;

    float moveSpeed = 0.5f;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerState = GetComponent<PlayerState>();
        playerRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //move
        //transform.position += new Vector3(playerInput.moveHorizontalValue * 0.01f, 0f, playerInput.moveVerticalValue * 0.01f);
        if (playerInput.moveVerticalValue > 0)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (playerInput.moveVerticalValue < 0)
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (playerInput.moveHorizontalValue > 0)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else if (playerInput.moveHorizontalValue < 0)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        //sit

        //jump
        if (playerInput.isJump)
        {
            if (!playerState.inWater)
            {
                if (!playerState.inWaterSurface)
                {
                    playerRigid.AddForce(Vector3.up * 100f);
                }
                else
                {
                    if (playerState.canJumpOnFoundation && !playerState.isJumping)
                    {
                        //Jump to foundation
                        playerState.isJumping = true;
                        StartCoroutine(PlayerJumpOnFoundation_co());
                        playerRigid.AddForce(Vector3.up * 100f);
                    }
                }
            }
            else //in water
            {
                playerRigid.AddForce(Vector3.up * 30f);
            }

        }
        if (playerState.inWaterSurface && !playerState.isJumping)
        {
            if (playerInput.moveVerticalValue > 0 && transform.GetChild(0).localRotation.x > 0.12)
            {
                transform.position -= new Vector3(0f, -0.1f, 0f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, -0.599f, transform.position.z);
            }
        }

        //player rigidbody
        if (transform.position.y > -0.437f)
        {
            playerRigid.useGravity = true;
        }
        else
        {
            playerRigid.useGravity = false;
        }
    }
    IEnumerator PlayerJumpOnFoundation_co()
    {
        float time = 1f;
        float forward = -0.002f;
        float up = 0.00533f;
        float jumpTime = 0f;

        while (jumpTime < time)
        {
            playerRigid.velocity = Vector3.zero;
            transform.position += Vector3.forward * forward;
            transform.position += Vector3.up * up;
            jumpTime += 0.01f;
            yield return null;
        }
        playerState.isJumping = false;
    }

}
