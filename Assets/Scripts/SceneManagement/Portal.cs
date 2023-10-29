using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        /// <summary>
        /// Set Destination to distinct which portal player will come in next scene
        /// Example: Player go to Portal A in scene 1 then come to Portal A scene 2 
        /// This very usefull when you have many portal
        /// </summary>
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }
        //Index scene load (check in File -> Build Setting)
        [SerializeField] private int sceneToLoad = -1;
        //spawn point of player when go out of that portal  
        [SerializeField] private Transform spawnPoint;
        //destination to distinct which Portal in scene
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

                //Create Faded effect
                Faded faded =  FindObjectOfType<Faded>();
                //Do effect fade out 
                yield return faded.FadeOut(fadeOutTime);

                //save last scene
                SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
                savingWrapper.Save();
                
                //Load new scene 
                yield return SceneManager.LoadSceneAsync(sceneToLoad);

                //load new scene
                savingWrapper.Load();

                //Get another Portal (on another scene)
                Portal otherPortal = GetAnotherPortal();
                //Update position player to portal in next scene
                UpdatePlayer(otherPortal);

                //save new scene (to load when restart)
                savingWrapper.Save();

                //Wait a little time before Fade in (avoid some problem when load to another scene)
                yield return new WaitForSeconds(waitingTime);
                //Effect fade in
                yield return faded.FadeIn(fadeInTime);
                //Destroy this gameobject (portal) when loading done 
                Destroy(gameObject);
               
            }
        }
        
        //Update Player position in new portal 
        private void UpdatePlayer(Portal otherPortal)
        {
            //Find the player
            GameObject player = GameObject.FindWithTag("Player");
            //Must turn off navmeshAgent to avoid problem when change position and rotatation 
            player.GetComponent<NavMeshAgent>().enabled = false;
            //Set Player position to Spawn point of that portal position 
            //CAUTION: Must push portal Object out of everthing and not have Parent, if you don't the position player won't set
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            //Turn on again 
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

