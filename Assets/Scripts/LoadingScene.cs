using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene instance;

    public GameObject loadingScreen;
    public Slider loadingSlider;
    public TMP_Text loadingText;

    private float progress;

    public void LoadLevel(int index)
    {
        StartCoroutine(LoadAsync(index));
    }

    IEnumerator LoadAsync(int index)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress);
            loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, progress, 3 * Time.deltaTime);
            loadingText.text = loadingSlider.value.ToString("0%");
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}
