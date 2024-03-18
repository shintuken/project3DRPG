using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Pick Up SFX")]
    [field: SerializeField] public EventReference pickUpWeapon { get; private set; }

    [field: Header("Player Footstep SFX")]
    [field: SerializeField] public EventReference footstep { get; private set; }

    [field: Header("Weapon Attack Sound")]
    [field: SerializeField] public EventReference swordAtk { get; private set; }
    [field: SerializeField] public EventReference bowAtk { get; private set; }
    [field: SerializeField] public EventReference fireballAtk { get; private set; }
    [field: SerializeField] public EventReference unarmedAtk { get; private set; }

    [field: Header("Enemy Death")]
    [field: SerializeField] public EventReference enemyDeath { get; private set; }

    [field: Header("Button")]
    [field: SerializeField] public EventReference sfxBtnClick { get; private set; }
    [field: SerializeField] public EventReference sfxBtnHover { get; private set; }

    [field: Header("Winning/Losing Sound")]
    [field: SerializeField] public EventReference winSound { get; private set; }
    [field: SerializeField] public EventReference loseSound { get; private set; }
    
    [field: Header("Winning/Losing Sound")]
    [field: SerializeField] public EventReference bgmGameplay { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in scene");
        }
        instance = this;
    }

}
