using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmSlot : MonoBehaviour
{
    float fadeInTime = 1f;
    float timeFadeIn = 0f;

    Image backImage;
    Text count;
    Image itemImage;
    Text itemName;
    
    private void OnEnable()
    {
        backImage = GetComponent<Image>();

        count = transform.GetChild(0).GetComponent<Text>();
        itemImage = transform.GetChild(1).GetComponent<Image>();
        itemName = transform.GetChild(2).GetComponent<Text>();

        StartCoroutine(FadeOut_co());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut_co());
    }

    IEnumerator FadeOut_co()
    {
        Color imgColorBack = backImage.color;
        float imgColorBackA = backImage.color.a;

        Color imgColorChild = count.color;
        imgColorChild.a = 1f;

        while (imgColorChild.a > 0f)
        {
            timeFadeIn += Time.deltaTime / fadeInTime;

            imgColorBack.a = Mathf.Lerp(imgColorBackA, 0f, timeFadeIn);
            backImage.color = imgColorBack;
            
            imgColorChild.a = Mathf.Lerp(1f, 0f, timeFadeIn);
            count.color = imgColorChild;
            itemImage.color = imgColorChild;
            itemName.color = imgColorChild;
            yield return null;
        }
        Destroy(gameObject);
    }
}
