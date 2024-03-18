using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private EventInstance musicEventInstance;

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

    public EventInstance CreatEventInstance(EventReference eventReference) 
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    public void StopSound(EventReference sound)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(sound); // Tạo EventInstance từ EventReference
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Dừng âm thanh và cho phép fade-out
        eventInstance.release(); // Giải phóng EventInstance
    }

}
