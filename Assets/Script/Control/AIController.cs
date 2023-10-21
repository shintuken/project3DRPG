using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Threading;
using System;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chasingDistance = 3f;
        [SerializeField] private float chasingSpeed = 10f;
        [Range(0f, 1f)]
        [SerializeField] private float moveSpeedPatrolFraction = .2f;
        [SerializeField] private float moveSpeedChasingFraction = .8f;


        private GameObject player;

        private Fighter fighter;
        private Mover mover;

        private Health healthTarget;
        private Vector3 guardPosition;
//        private Vector3 firstGuardPosition;

        //Patrol Path
        [SerializeField] private PatrolPath patrolPath; 
        private int currentPatrolPoint = 0;
        [SerializeField] private float PatrolRadiusAccept = 2f;


         private float timeSinceLastedSawPlayer = Mathf.Infinity;
        [SerializeField] private float supiciousTime = 5f;

        private float timeSinceLastedWait = Mathf.Infinity;
        [SerializeField] private float patrolWaitingTime = 3f;


        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            healthTarget = GetComponent<Health>();
            guardPosition =  transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (healthTarget.IsDeath()) return;
            if (InRangeAttackOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastedSawPlayer = 0f;
                AttackBehaviour();
            }
            else if (timeSinceLastedSawPlayer < supiciousTime)
            {
                SupicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }
            CountingTime();
        }

        private void CountingTime()
        {
            timeSinceLastedWait += Time.deltaTime;
            timeSinceLastedSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            //cancel attack and move to guard position
            if (patrolPath == null) return;
            if (IsInPatrolPosition())
            {
                CyclePosition();
            }
            guardPosition = GetCurrentState();
            if (timeSinceLastedWait > patrolWaitingTime)
            {
                timeSinceLastedWait = 0f;
                mover.StartMoveAction(guardPosition, moveSpeedPatrolFraction);
            }

        }

        private Vector3 GetCurrentState()
        {
           return patrolPath.GetPointPosition(currentPatrolPoint);
        }

        private void CyclePosition()
        {
            currentPatrolPoint = patrolPath.CyclePoint(currentPatrolPoint);
        }

        private bool IsInPatrolPosition()
        {
            return Vector3.Distance(transform.position, GetCurrentState()) < PatrolRadiusAccept;
        }

        private void SupicionBehaviour()
        {
            //Supicious State - Wait in there to think where you go
            //Cancel any action doing now
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        //CHeck range attack enemy 
        private bool InRangeAttackOfPlayer()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance < chasingDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chasingDistance);

            //Gizmos.DrawSphere(transform.position, chasingDistance);
        }

    }
}

