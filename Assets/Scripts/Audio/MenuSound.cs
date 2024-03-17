using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{

    [field: Header("Background Music")]
    [field: SerializeField] public EventReference menuBGM { get; private set; }

    [field: Header("Button Click")]
    [field: SerializeField] public EventReference sfxBtnClick { get; private set; }

    [field: Header("Button Hover")]
    [field: SerializeField] public EventReference sfxBtnHover { get; private set; }


    public static MenuSound instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There is more than 1 MenuSound in scene");
        }
        else
        {
            instance = this;
        }
   
    }


}
