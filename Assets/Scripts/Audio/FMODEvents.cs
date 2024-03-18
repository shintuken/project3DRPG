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

    [field: Header("Sword Attack")]
    [field: SerializeField] public EventReference swordAtk { get; private set; }

    [field: Header("Bow Attack")]
    [field: SerializeField] public EventReference bowAtk { get; private set; }

    [field: Header("Fireball Attack")]
    [field: SerializeField] public EventReference fireballAtk { get; private set; }

    [field: Header("Unarmed Attack")]
    [field: SerializeField] public EventReference unarmedAtk { get; private set; }

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
