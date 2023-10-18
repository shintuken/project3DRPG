using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chasingDistance = 3f;
        [SerializeField] private float supiciousTime = 5f;

        private GameObject player;

        private Fighter fighter;
        private Mover mover;

        private Health healthTarget;
        private Vector3 guardPosition;

        [SerializeField] private float timeSinceLastedSawPlayer = Mathf.Infinity;


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
                GuardBehaviour();
            }

            timeSinceLastedSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            //cancel attack and move to guard position
            mover.StartMoveAction(guardPosition);
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

