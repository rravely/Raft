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
            playerRigid.AddForce(Vector3.up * 200f); 
        }
        if (playerState.inWaterSurface)
        {
            transform.position = new Vector3(transform.position.x, -0.599f, transform.position.z);
        }
    }
}
