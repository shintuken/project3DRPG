using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] private int sceneToLoad = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationIdentifier destination;

        [SerializeField] private float fadeInTime = 1f;
        [SerializeField] private float fadeOutTime = 1f;
        [SerializeField] private float waitingTime = .5f;





        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(LoadScene());
            }


            IEnumerator LoadScene()
            {
                if (sceneToLoad < 0)
                {
                    Debug.LogError("Screen To Load is not set ");
                    yield break;
                }
                
                DontDestroyOnLoad(gameObject);

                Faded faded =  FindObjectOfType<Faded>();
                //Effect fade out
                yield return faded.FadeOut(fadeOutTime);
                yield return SceneManager.LoadSceneAsync(sceneToLoad);

                Portal otherPortal = GetAnotherPortal();
                UpdatePlayer(otherPortal);

                //Effect fade in
                yield return new WaitForSeconds(waitingTime);
                yield return faded.FadeIn(fadeInTime);

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
                //If this is last portal, next from other
                if (portal == this) continue;
                //Check the destination portal (distinguish map have more than 1 portal)
                if(portal.destination != destination) continue;

                return portal;
            }
            return null;

        }
    }
}

