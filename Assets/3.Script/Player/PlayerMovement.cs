using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerState playerState;
    Rigidbody playerRigid;
    Collider[] playerCollider;

    float moveSpeed = 0.5f;

    bool isStandingUp = false;

    PlayerInputAction playerInputAction;
    InputAction jumpAction;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerState = GetComponent<PlayerState>();
        playerRigid = GetComponent<Rigidbody>();
        playerCollider = GetComponents<Collider>();

        playerInputAction = new PlayerInputAction();
        jumpAction = playerInputAction.Player.Jump;
    }

    private void Update()
    {
        //move
        //transform.position += new Vector3(playerInput.moveHorizontalValue * 0.01f, 0f, playerInput.moveVerticalValue * 0.01f);
        if (!playerState.isSleep)
        {
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
        }

        //sit
        /*
        if (playerInput.isSit && !playerState.inWater && !playerState.inWaterSurface)
        {
            playerCollider[0].enabled = false;
        }
        if (!playerInput.isSit && !isStandingUp)
        {
            isStandingUp = true;
            StartCoroutine(PlayerStandUp_co());
        }
        */

        //Set player y position inWaterSurface
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

    void OnJump()
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

    IEnumerator PlayerJumpOnFoundation_co()
    {
        float time = 1f;
        float forward = -0.002f;
        float up = 0.00533f;
        float jumpTime = 0f;

        PlayerAudio.instance.WaterPlump();

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

    IEnumerator PlayerStandUp_co()
    {
        Vector3 newPos = transform.position;
        while(transform.position.y < -0.2602059f)
        {
            //Debug.Log(transform.position.y);
            newPos += new Vector3(0f, 0.01f, 0f);
            transform.position = newPos;
            if (transform.position.y > -0.250)
            {
                playerCollider[0].enabled = true;
                break;
            }
            yield return null;
        }
        isStandingUp = false;
    }

}
