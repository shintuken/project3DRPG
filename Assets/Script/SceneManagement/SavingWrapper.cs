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
        const string SaveFileName = "save";

        private void Start()
        {
            savingSystem = GetComponent<SavingSystem>();

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

        private void Save()
        {

            savingSystem.Save(SaveFileName);
        }

        private void Load()
        {

            savingSystem.Load(SaveFileName);
        }
    }
}

