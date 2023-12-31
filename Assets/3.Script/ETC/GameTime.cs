using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameTime : MonoBehaviour
{
    float dayTime = 480f;
    float currTime = 0f;
    float morningTime, nightTime, hour;

    bool isChanging = false;
    bool isChanged = false;

    Volume volume;
    ColorAdjustments colorAdjustments;

    Color day= Color.white;
    Color night = new Color(0.039f, 0.039f, 0.039f);

    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        morningTime = dayTime / 24 * 5; // 5:00
        nightTime = dayTime / 6 * 5; // 20:00
        hour = dayTime / 24; //1 hour

        currTime = morningTime + hour * 2;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if (currTime > dayTime)
        {
            dayTime = 0f;
        }

        if (currTime >= morningTime && currTime <= nightTime) //5:00 ~ 20:00
        {
            colorAdjustments.colorFilter.value = day;
            isChanging = false;
            isChanged = false;
        }
        else if (currTime >= nightTime + hour || currTime <= morningTime - hour) // 20:00 ~ 5:00
        {
            colorAdjustments.colorFilter.value = night;
            isChanging = false;
            isChanged = false;
        }
        else if (currTime > morningTime && currTime < morningTime + hour && !isChanging && !isChanged) //5:00 ~ 6:00
        {
            isChanging = true;
            StartCoroutine(ChangeNightToDay_co());
        }
        else if (currTime > nightTime && currTime < nightTime + hour && !isChanging && !isChanged)
        {
            isChanging = true;
            StartCoroutine(ChangeDayToNight_co());
        }
    }

    IEnumerator ChangeNightToDay_co()
    {
        float progress = 0f;

        while (progress < 1)
        {
            colorAdjustments.colorFilter.value = Color.Lerp(night, day, progress);
            progress += Time.deltaTime / hour;
            yield return null;
        }
        isChanging = false;
        isChanged = true;
    }

    IEnumerator ChangeDayToNight_co()
    {
        float progress = 0f;

        while (progress < 1)
        {
            colorAdjustments.colorFilter.value = Color.Lerp(day, night, progress);
            progress += Time.deltaTime / hour;
            yield return null;
        }
        isChanging = false;
        isChanged = true;
    }

    public void SetMorningTime()
    {
        currTime = morningTime + hour;
    }

    public bool PlayerCanSleep()
    {
        if (currTime > nightTime || currTime < morningTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayerSleep()
    {
        StopAllCoroutines();
        currTime = morningTime;
    }
}
