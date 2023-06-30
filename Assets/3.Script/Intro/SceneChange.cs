using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Image sceneLoadingImg;
    [SerializeField] Slider progressBar;
    [SerializeField] Image loadingIcon;

    public void NewWorld()
    {
        sceneLoadingImg.gameObject.SetActive(true);
        StartCoroutine(LoadScene_co(sceneName));
    }

    IEnumerator LoadScene_co(string sceneName)
    {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);

        Color imgColor = sceneLoadingImg.color;
        imgColor.a = 1f;
        sceneLoadingImg.enabled = true;

        while (!asyncOper.isDone)
        {
            //Debug.Log(string.Format("Scene Load: {0}", asyncOper.progress));
            progressBar.value = asyncOper.progress;
            loadingIcon.rectTransform.eulerAngles += new Vector3(0f, 0f, 5f);
            yield return null;
        }
    }
}
