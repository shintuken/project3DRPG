using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGM : MonoBehaviour
{


    void Start()
    {
        AudioManager.instance.PlayOneShotNoPosition(MenuSound.instance.menuBGM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
