using RPG.Core;
using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float healthPoints;
        private bool isDeath = false;
        //GET UNIQUE ID
        public string uniqueID = Guid.NewGuid().ToString();

        private void Awake()
        {
            maxHealth = GetComponent<BaseStat>().GetHealth();
            healthPoints = GetComponent<BaseStat>().GetHealth();
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public bool IsDeath()
        {
            return isDeath;
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public void SetHealth(float health)
        {
            healthPoints = health;
        } 

        public float GetHealthFraction()
        {
            return healthPoints / maxHealth;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Death();
            }
        }

        private void Death()
        {
            if (isDeath) return;

            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            isDeath = true;
        }

        public object CaptureState()
        {
            return healthPoints;
        }
        public void RestoreState(object state)
        {
           healthPoints = (float)state;
           
           if(healthPoints <= 0)
           {
                Death();
           }
        }

    }
}

