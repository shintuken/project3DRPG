using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private SavingSystem savingSystem;
        private float fadeInTime = 0.2f;
        private Faded fader;
        const string SaveFileName = "save";

        private void Awake()
        { 
            savingSystem = GetComponent<SavingSystem>();
/*            fader = FindObjectOfType<Faded>();
            fader.FadeOutImmediately();*/
        }

        IEnumerator Start()
        {
            
            yield return GetComponent<SavingSystem>().LoadLastScene(SaveFileName);
           // yield return fader.FadeIn(fadeInTime);

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {

            savingSystem.Save(SaveFileName);
        }

        public void Load()
        {

            savingSystem.Load(SaveFileName);
        }
    }
}

