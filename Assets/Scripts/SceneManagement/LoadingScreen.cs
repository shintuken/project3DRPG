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
        Debug.Log("going in Load Scene");
        StartCoroutine(LoadingSceneAsync(sceneId));
    }


    IEnumerator LoadingSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        LoadingScene.SetActive(true);
        while (!operation.isDone)
        { 
            Debug.Log("while !operation.isDOne");        
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            LoadingBarFill.fillAmount = progressValue;
            
            yield return null;
            Debug.Log("Completed");
        }
    }
}
