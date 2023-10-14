using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        //Chasing distance 
        [SerializeField] private float chasingDistance = 3f;

        //Patrol Class
        [SerializeField] private PatrolPath patrolPath;

        //Player
        private GameObject player;

        //Fighter and Mover
        private Fighter fighter;
        private Mover mover;

        //Enemy health
        private Health healthTarget;
        
        //Enemy guard position
        private Vector3 guardPosition;

        //Time wait when Player out of range chasing
        [SerializeField] private float supiciousTime = 5f;
        private float timeSinceLastedSawPlayer = Mathf.Infinity;

        //Time watting at each patrol point
        [SerializeField] private float dwellingTimeMax = 3f;
        private float dwellingTime = Mathf.Infinity;
        
        //Minium distance between Enemy and Patrol point that Enemy begin to patrol
        private float wayPointTolerance = 1f;

        //Current Patrol Point
        private int currentWayPointIndex = 0;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();  
            healthTarget = GetComponent<Health>();
            guardPosition =  this.transform.position;
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
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            dwellingTime += Time.deltaTime;
            timeSinceLastedSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if(patrolPath != null)
            {
                if (AtWayPoint())
                {
                    dwellingTime = 0f;
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWayPoint();

            }
            if (dwellingTime > dwellingTimeMax)
            {
                mover.StartMoveAction(nextPosition);            
            }
            
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWayPoint(currentWayPointIndex);
        }

        private void CycleWayPoint()
        {
          currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointTolerance; 
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

        //Unity function -> Draw Gizmos 
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chasingDistance);

            //Gizmos.DrawSphere(transform.position, chasingDistance);
        }

    }
}

