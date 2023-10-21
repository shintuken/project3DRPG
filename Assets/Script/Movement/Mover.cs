using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        //[SerializeField] private Transform target;
        private NavMeshAgent navMeshAgent;
        private Health healthTarget;
        private float maxSpeed = 6f;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();   
            healthTarget = GetComponent<Health>();
        }

        private void Update()
        {
            navMeshAgent.enabled = !healthTarget.IsDeath();
            UpdateAnimator();
        }


        //moveSpeedFraction = 1 mean the speed default always 100%
        public void StartMoveAction(Vector3 destination, float moveSpeedFraction = 1)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, moveSpeedFraction);
        }

        //moveSpeedFraction = 1 mean the speed default always 100%
        public void MoveTo(Vector3 destination, float moveSpeedFraction = 1)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(moveSpeedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            //Lấy velocity từ NavMesh Agentf
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            //Convert global sang localVelocity
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            //Lấy speed = localVelocity.Z
            float speed = localVelocity.z;
            //Set Float của animator
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

    }
    
    
}

