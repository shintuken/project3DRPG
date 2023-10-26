using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistenceObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject persistenceObjectPrefab;

        static bool hasSpawned = false;


        void Awake()
        {
            if (!hasSpawned)
            {
                SpawnPersistenceObject();
                hasSpawned = true;
            }
        }

        private void SpawnPersistenceObject()
        {
            GameObject persistenceObject = Instantiate(persistenceObjectPrefab);
            DontDestroyOnLoad(persistenceObject);   
        }
    }
}

