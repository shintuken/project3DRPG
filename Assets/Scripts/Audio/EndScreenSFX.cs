using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenSFX : MonoBehaviour
{
   public void ButtonClick()
    {
        AudioManager.instance.PlayOneShotNoPosition(FMODEvents.instance.sfxBtnClick);
    }
    public void ButtonHover()
    {
        AudioManager.instance.PlayOneShotNoPosition(FMODEvents.instance.sfxBtnHover);
    }

}
