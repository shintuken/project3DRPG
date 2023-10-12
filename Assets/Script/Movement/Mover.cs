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

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();   
        }

        private void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
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

