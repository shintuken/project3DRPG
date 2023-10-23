using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicRemoveControl : MonoBehaviour
    {
        //Get player 
        private GameObject player;
        private void Start()
        {
            //get player
            player = GameObject.FindWithTag("Player");
            //Disable control when play cinematic
            GetComponent<PlayableDirector>().played += DisableControl;
            //Enable control when off cinematic
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        public void DisableControl(PlayableDirector pd)
        {
            //Cancel player action
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        public void EnableControl(PlayableDirector pd)
        {         
            //Enable player action again
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}

