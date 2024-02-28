using RPG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using Unity.VisualScripting;
using RPG.Resources;
using FMOD.Studio;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health healthTarget;
        [SerializeField] private float moveSpeedFraction = 1;
        [SerializeField] private float moveSpeed = 1;

        private void Start()
        {
            healthTarget = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (healthTarget.IsDeath()) return;
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        //For Cheat manager
        public void UpdateSpeedFraction(float speedFraction)
        {
            moveSpeedFraction = speedFraction;
        }

        private bool InteractWithCombat()
        {         
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    GameObject enemy = hit.transform.gameObject;
                    GetComponent<Fighter>().Attack(enemy);
                }

                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
                RaycastHit hit;
                bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
                if (hasHit)
                {
                    if (Input.GetMouseButton(1))
                    {
                        GetComponent<Mover>().StartMoveAction(hit.point, moveSpeed, moveSpeedFraction);
                    }
                    return true;
                }
                return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

