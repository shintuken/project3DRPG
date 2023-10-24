using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad = -1;
        [SerializeField] private Transform spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(LoadScene());
            }


            IEnumerator LoadScene()
            {
                //DontDestroyOnLoad dosen't work when the GameObject is child of something, so must set Parent to NULL
                //transform.parent = null;

                DontDestroyOnLoad(gameObject);
                yield return SceneManager.LoadSceneAsync(sceneToLoad);

                Portal otherPortal = GetAnotherPortal();
                UpdatePlayer(otherPortal);

                Destroy(gameObject);
            }
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;

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
}

