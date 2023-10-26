using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using EZCameraShake;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float timeBetweenAttack = 2f;
        [SerializeField] private float weaponDamage = 5f;

        //Không cần dùng transform mà dùng trực tiếp Health để sử dụng 
        Health target;
        //Set infinity so when start object will attack immediately without waitting time
        float timeSinceLastAttack = Mathf.Infinity;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            //We don't have Target
            if (target == null) return;
            //Target already death
            if (target.IsDeath()) return;
            //Move to target range
            if (!GetInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            //Get in range and attack
            else
            {
                GetComponent<Mover>().Cancel();             
                AttackBehaviour();           
            }
        }

        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;
            Health testTarget = target.GetComponent<Health>();
            return (testTarget != null && !testTarget.IsDeath());
        }

        private void AttackBehaviour()
        {
            //Look at enemy 
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttack)
            {
                //this will trigger HIT() event
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }

        }

        private void TriggerAttack()
        {
            //Stop animation Attack
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attacking");
            //this will trigger Hit event
        }

        // Animation attack effect
        void Hit()
        {
            if(target != null)
            {
                //Damage on target
                target.TakeDamage(weaponDamage);
                //Shake camera 
                CameraShaker.Instance.ShakeOnce(3f, 3f, .1f, 1);
            }    
        }

        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < attackRange;
        }

        public void Attack(GameObject Target)
        {

            GetComponent<ActionScheduler>().StartAction(this);
            target = Target.GetComponent<Health>();
        }

        public void Cancel()
        {
            TriggerStopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void TriggerStopAttack()
        {
            //Set animation Attack 
            GetComponent<Animator>().SetTrigger("StopAttack");
            //Reset Trigger StopAttack
            GetComponent<Animator>().ResetTrigger("Attacking");
        }


    }
}

