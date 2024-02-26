using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in scene");
        }
        instance = this; 
    }

    public void PlayOneShot(EventReference sound, Vector3 postion)
    {
      RuntimeManager.PlayOneShot(sound, postion);
    }
    public void PlayOneShotNoPosition(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }
}
