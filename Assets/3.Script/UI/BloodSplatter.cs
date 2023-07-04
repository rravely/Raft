using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodSplatter : MonoBehaviour
{
    Image bloodSplatterImage;

    //Fade in
    float fadeInTime = 1f;
    float timeFadeIn = 0f;

    private void Start()
    {
        bloodSplatterImage = GetComponent<Image>();
    }

    public void BloodSplat()
    {
        StartCoroutine(BloodSplatter_co());
    }

    IEnumerator BloodSplatter_co()
    {
        Color imgColor = bloodSplatterImage.color;
        imgColor.a = 1f;

        while (imgColor.a > 0f)
        {
            timeFadeIn += Time.deltaTime / fadeInTime;
            imgColor.a = Mathf.Lerp(1f, 0f, timeFadeIn);
            bloodSplatterImage.color = imgColor;
            yield return null;
        }

        imgColor.a = 0f;
        bloodSplatterImage.color = imgColor;

        fadeInTime = 1f;
        timeFadeIn = 0f;
    }
}
