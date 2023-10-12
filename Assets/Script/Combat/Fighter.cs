using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float timeBetweenAttack = 2f;
        [SerializeField] private float weaponDamage = 5f;

        Transform target;
        float timeSinceLastAttack = 0;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;

            if (!GetInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
                
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                //this will trigger hit event
                GetComponent<Animator>().SetTrigger("Attacking");

                timeSinceLastAttack = 0f;
            }

        }

        // Animation attack effect
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }

        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, target.position) < attackRange;
        }

        public void Attack(CombatTarget CombatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = CombatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        

    }
}

