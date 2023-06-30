using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGame : MonoBehaviour
{
    [SerializeField] GameObject escPanel;
    bool isActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
        }

        if (isActive)
        {
            escPanel.SetActive(true);
        }
        else
        {
            escPanel.SetActive(false);
        }
    }

    public void BackToGameButton()
    {
        isActive = false;
        escPanel.SetActive(false);
    }
}
