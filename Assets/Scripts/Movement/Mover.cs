﻿using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Attributes;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxNavMeshLength = 40f;
        
        NavMeshAgent navMeshAgent;
        Health health;
        Transform target;

        void Start()
        {
           navMeshAgent = GetComponent<NavMeshAgent>();
           health = GetComponent<Health>();
        }


        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination,speedFraction);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) { return false; }
            if (path.status != NavMeshPathStatus.PathComplete) { return false; }
            if (GetPathLength(path) > maxNavMeshLength) { return false; }

            return true;
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float totalLength = 0;
            if (path.corners.Length < 2) { return totalLength; }

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                float sectionLength = Vector3.Distance(path.corners[i], path.corners[i + 1]);
                totalLength += sectionLength;
            }
            return totalLength;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            transform.GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
