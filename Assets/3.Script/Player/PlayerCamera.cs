using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PlayerInput playerInput;

    [SerializeField] GameObject craftMenu;
    [SerializeField] GameObject escPanel;
    [SerializeField] GameObject structureMenu;

    float xRotateSize, yRotateSize = 0f;
    float xRotate, yRotate = 0f;
    float turnSpeed = 2f;

    [SerializeField] Texture2D cursorImg;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!craftMenu.activeSelf && !escPanel.activeSelf && !structureMenu.activeSelf)
        {
            Cursor.visible = false;

            xRotateSize = -playerInput.mouseVertical * turnSpeed;
            xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 45);

            yRotateSize = playerInput.mouseHorizontal * turnSpeed;
            yRotate = transform.eulerAngles.y + yRotateSize;

            transform.eulerAngles = new Vector3(0, yRotate, 0);
            transform.GetChild(0).eulerAngles = new Vector3(xRotate, yRotate, 0);
        }
        else
        {
            Cursor.visible = true;
        }
    }
}
