using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int sceneToLoad = -1;
    [SerializeField] private Transform spawnPoint;
    private GameObject player;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadScene());
        }


        IEnumerator LoadScene()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            DontDestroyOnLoad(this.gameObject);
            yield return asyncOperation;

            Portal otherPortal = GetAnotherPortal();
            UpdatePlayer(otherPortal);
                            
            Destroy(this.gameObject);
        }
    }

    private void UpdatePlayer(Portal otherPortal)
    {
        player = GameObject.FindWithTag("Player");
        if (otherPortal != null)
        { 
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;

        }
    }

    private Portal GetAnotherPortal()
    {
        foreach (Portal portal in FindObjectsOfType<Portal>())
        {
            if (portal == this) continue;
            return portal;
        }
        return null;
       
    }
}
