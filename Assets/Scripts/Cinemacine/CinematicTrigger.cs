using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematic
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        private bool isPlayed = false;
      
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player" && !isPlayed)
            {
                GetComponent<PlayableDirector>().Play();
                isPlayed = true;
            }
        
        }

        //Saving 
        public object CaptureState()
        {
            return isPlayed;

        }

        //Loading
        public void RestoreState(object state)
        {
            isPlayed = (bool)state;
        }
    }
}

