using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string moveVertical = "Vertical"; //ws
    private string moveHorizontal = "Horizontal"; //ad
    private string interaction = "Interaction";
    private string jump = "Jump";
    private string sit = "Fire1";
    private string mouseX = "Mouse X";
    private string mouseY = "Mouse Y";

    //GetAxis -> return float data type
    public float moveVerticalValue { get; private set; }
    public float moveHorizontalValue { get; private set; }

    //GetButton -> return bool data type
    public bool isInteraction { get; private set; }
    public bool isJump { get; private set; }
    public bool isSit { get; private set; }
    public bool isStandUp { get; private set; }

    public bool isLMD { get; private set; }
    public bool isLMDUp { get; private set; }
    public bool isLMDDown { get; private set; }
    public bool isRMD { get; private set; }
    public bool isRMDUp { get; private set; }
    public bool isRMDDown { get; private set; }

    public float mouseHorizontal { get; private set; }
    public float mouseVertical { get; private set; }

    // Update is called once per frame
    void Update()
    {
        //move
        moveVerticalValue = Input.GetAxis(moveVertical);
        moveHorizontalValue = Input.GetAxis(moveHorizontal);

        //key down
        isInteraction = Input.GetButtonDown(interaction);
        isJump = Input.GetButtonDown(jump);
        isSit = Input.GetButton(sit);
        isStandUp = Input.GetButtonUp(sit);

        isLMD = Input.GetMouseButton(0);
        isLMDUp = Input.GetMouseButtonUp(0);
        isLMDDown = Input.GetMouseButtonDown(0);
        isRMD = Input.GetMouseButton(1);
        isRMDDown = Input.GetMouseButtonDown(1);

        mouseHorizontal = Input.GetAxis(mouseX);
        mouseVertical = Input.GetAxis(mouseY);
    }
}
