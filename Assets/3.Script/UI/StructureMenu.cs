using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StructureMenu : MonoBehaviour
{
    SelectedItem selectedItem;

    PlayerInputAction playerInputAction;
    InputAction rightMoustButtonAction;

    private void Start()
    {
        selectedItem = FindObjectOfType<SelectedItem>();

        playerInputAction = new PlayerInputAction();
        rightMoustButtonAction = playerInputAction.Player.RightMouse;
    }

    void OnRightMouse()
    {

    }
}
