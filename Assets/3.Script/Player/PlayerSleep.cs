using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSleep : MonoBehaviour
{
    PlayerState playerState;
    [SerializeField] GameTime gameTime;

    [SerializeField] Image fadeImage;

    [SerializeField] GameObject vcam;

    [SerializeField] GameObject[] playerHands;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    public void PlayerLieOnBed(Vector3 liePos)
    {
        //Vcam move
        playerState.isSleep = true;
        vcam.SetActive(true);
        vcam.transform.position = liePos;

        //Player Hands inactivate
        ActivateHands(false);

        if (gameTime.PlayerCanSleep())
        {
            PlayerGoSleep();
        }
    }

    public void PlayerGetUp(Vector3 standPos)
    {
        //vcam move
        vcam.SetActive(false);

        ActivateHands(true);

        playerState.isSleep = false;
    }

    public void PlayerGoSleep()
    {
        StartCoroutine(SleepFade_co());
    }

    IEnumerator SleepFade_co()
    {
        fadeImage.gameObject.SetActive(true);

        //fade
        float time = 0f;
        Color imgColor = fadeImage.color;
        while (time < 2f)
        {
            fadeImage.color = imgColor;

            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);

        //Set Time
        gameTime.SetMorningTime();

        //Change player stat
        playerState.health = 100f;
    }

    void ActivateHands(bool isActive)
    {
        playerHands[0].SetActive(isActive);
        playerHands[1].SetActive(isActive);
    }
}
