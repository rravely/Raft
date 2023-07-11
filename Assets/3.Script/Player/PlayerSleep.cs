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

        if (gameTime.PlayerCanSleep())
        {
            PlayerGoSleep();
        }
    }

    public void PlayerGetUp(Vector3 standPos)
    {
        //vcam move
        vcam.SetActive(false);

        playerState.isSleep = false;
    }

    public void PlayerGoSleep()
    {
        Debug.Log("Sleep");
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
}
