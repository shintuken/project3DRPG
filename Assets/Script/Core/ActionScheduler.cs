using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        //Check current action
        public void StartAction(IAction action)
        {
            //if current action already = action => Don't need to check anymore
            if (currentAction == action) return;

            //if we have current action
            if(currentAction != null ) 
            {
              currentAction.Cancel();
            }
            //Set new currtent action after player change behaviour 
            currentAction = action;
        }
    }
}

