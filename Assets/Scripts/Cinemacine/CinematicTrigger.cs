using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicTrigger : MonoBehaviour
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

    }
}

