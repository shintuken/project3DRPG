﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;
using RPG.Saving;
using RPG.Resources;
using Unity.VisualScripting;
using FMODUnity;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] private Transform righthandWeapon = null;
        [SerializeField] private Transform lefthandWeapon = null;
        [SerializeField] private Weapon defaultWeapon = null;
        [SerializeField] private string defaultWeaponName = "Unarmed";

        //Get Damage Text Effect 
        [SerializeField] private DynamicTextData dynamicTextData;
        [SerializeField] private float TextOffset = 5;


        private Transform hpBarTransform;
        private Vector3 damageTextPosition;


        private const string Bow = "bow";
        private const string FireBall = "firestaff";
        private const string Sword = "sword";
        private const string Unarmed = "unarmed";



        //Không cần dùng transform mà dùng trực tiếp Health để sử dụng 
        Health target;
        //Set infinity so when start object will attack immediately without waitting time
        float timeSinceLastAttack = Mathf.Infinity;
        //current weapon character used
        Weapon currentWeapon = null;

        private void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            hpBarTransform = transform.Find("HPBar");
        }
        //Update for GMT 
        public Weapon GetWeapon()
        {
            return currentWeapon;
        }     


        //spawn weapon
        public void EquipWeapon(Weapon weapon)
        {
            if(currentWeapon != null)
            {
                currentWeapon.RemoveWeapon();
            }
            currentWeapon = weapon;
            //create new animator
            Animator animator = GetComponent<Animator>();
            //Spawn animation
            // LOI SCENE 2
            currentWeapon.Spawn(righthandWeapon, lefthandWeapon, animator);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDeath())
            {
                // Thêm dòng này để ngăn chặn việc gọi UpdateHealthBarUI khi target không hợp lệ
                return;
            }
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
            Health combatTarget = target.GetComponent<Health>();
            return (combatTarget != null && !combatTarget.IsDeath());
        }

        private void AttackBehaviour()
        {
            //Look at enemy 
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > currentWeapon.GetTimeBetweenAttack())
            {
                //this will trigger HIT() event
                TriggerAttack();

                timeSinceLastAttack = 0f;
            }

        }

        //Manage Animatior event
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
            if (target == null) return;

            //Ranged weapon
            if (currentWeapon.HasProjectTile())
            {
                currentWeapon.LaunchProjectile(righthandWeapon, lefthandWeapon, target);

            }
            //Melee weapon
            else
            {
                //Damage on target
                target.TakeDamage(currentWeapon.GetWeaponDamage());
                //Update Damage text UI

            }
            //Update Damage text UI
            DamageTextUI();
            //Play Weapon SOUND
            PlayWeaponAttackSound();
        }

        private void DamageTextUI()
        {
            if (hpBarTransform != null)
            {
                Debug.Log("HPBAR not found");
                damageTextPosition = hpBarTransform.position;
            }
            else
            {
                damageTextPosition = new Vector3(target.transform.position.x, target.transform.position.y + TextOffset, target.transform.position.z);
            }
            DynamicTextManager.CreateText(damageTextPosition, "-" + currentWeapon.GetWeaponDamage().ToString(), dynamicTextData);
        }

        private void PlayWeaponAttackSound()
        {
            switch (currentWeapon.GetWeaponName())
            {
                case Sword:
                    AudioManager.instance.PlayOneShotNoPosition(FMODEvents.instance.swordAtk);
                    break;
                case Bow:
                    AudioManager.instance.PlayOneShotNoPosition(FMODEvents.instance.bowAtk);
                    break;
                case FireBall:
                    AudioManager.instance.PlayOneShotNoPosition(FMODEvents.instance.fireballAtk);
                    break;
                case Unarmed:
                    AudioManager.instance.PlayOneShotNoPosition(FMODEvents.instance.unarmedAtk);
                    break;
                default:
                    AudioManager.instance.PlayOneShotNoPosition(FMODEvents.instance.unarmedAtk);
                    break;
            }
        }



        //Trigger event when shoot arrow
        void Shoot()
        {
            //Manage when attack animation
            Hit();
        }

        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetAttackRange();
        }

        public void Attack(GameObject Target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = Target.GetComponent<Health>();
            
        }

        //GET TARGET
        public Health GetTarget()
        {
            return target;
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

        //Save
        public object CaptureState()
        {
            return currentWeapon.name;
        }
        //Load
        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

    }
}

