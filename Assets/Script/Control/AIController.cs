using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chasingDistance = 3f;
        private GameObject player;
        private Fighter fighter;
        private Health healthTarget;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
            healthTarget = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (healthTarget.IsDeath()) return;
            if (InRangeAttackOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                //Cancel attack when out of range
                fighter.Cancel();
            }
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }
}

