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
        [SerializeField] private float fadeInTime = 0.2f; 
        const string SaveFileName = "save";


        IEnumerator Start()
        {
            Faded fade = FindObjectOfType<Faded>();
            fade.FadeOutImmediately();
            yield return GetComponent<SavingSystem>().LoadLastScene(SaveFileName);
            yield return fade.FadeIn(fadeInTime);

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

            GetComponent<SavingSystem>().Save(SaveFileName);
        }

        public void Load()
        {

            GetComponent<SavingSystem>().Load(SaveFileName);
        }
    }
}

