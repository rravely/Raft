using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    Slider oxygenBar;

    PlayerState playerState;

    // Start is called before the first frame update
    private void OnEnable()
    {
        playerState = FindObjectOfType<PlayerState>();
        oxygenBar = transform.GetChild(0).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oxygenBar.value < 0.3)
        {
            transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        oxygenBar.value = playerState.oxygen * 0.01f;
    }
}
