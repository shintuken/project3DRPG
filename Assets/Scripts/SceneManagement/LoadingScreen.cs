using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    [SerializeField] private GameObject LoadingScene;
    [SerializeField] private Image LoadingBarFill;


    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadingSceneAsync(sceneId));
    }


    IEnumerator LoadingSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        LoadingScene.SetActive(true);
        yield return new WaitForSeconds(2);
        while (!operation.isDone)
        {     
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            LoadingBarFill.fillAmount = progressValue;
            
            yield return null;
        }
    }
}
