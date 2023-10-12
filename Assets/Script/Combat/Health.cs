using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        
        private bool isDeath = false;

        public bool IsDeath()
        {
           return isDeath;
        }


        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Death();
            }

        }

        private void Death()
        {
            if (isDeath) return;

            GetComponent<Animator>().SetTrigger("Death");
            isDeath = true;
        }

        
    }
}

