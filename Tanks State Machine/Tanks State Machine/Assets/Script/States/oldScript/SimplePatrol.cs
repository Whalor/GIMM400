using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.States
{
    public class SimplePatrol : MonoBehaviour
    {
        //decides wheter to wait on each point
        [SerializeField]
         bool _patrolWaiting;

        //the total time it waits on the point
        [SerializeField]
         float _totalWaitTime = 3f;


        //the probability of it switching directions
        [SerializeField]
        float _switchProbability = 0.2f;


        //list of the points
        [SerializeField]
        List<WayPoints> _patrolPoints;


        //private variables for base behavior
        [SerializeField]
        NavMeshAgent _navMeshAgent;
        int _currentPatrolIndex;
        bool _traveling;
        bool _waiting;
        bool _patrolForward;
        float _waitTimer;

        public List<WayPoints> PatrolPoints { get => _patrolPoints; set => _patrolPoints = value; }

        public void Start()
        {
           // _navMeshAgent = this.GetComponent<NavMeshAgent>();

            if(_navMeshAgent == null)
            {
                Debug.LogError("the nav mesh agent is not attached to " + gameObject.name);
                
            }
            else
            {
                if(_patrolPoints != null && _patrolPoints.Count >= 2)
                {
                    _currentPatrolIndex = 0;
                    SetDestination();
                }
                else
                {
                    Debug.LogError("Insufficient patrol points for basic patrolling");

                }
            }
        }

        public void Update()
        {
            //check if close to destination
            if(_traveling && _navMeshAgent.remainingDistance <= 1.0f)
            {
                _traveling = false;

                //if going to wait then wait
                if (_patrolWaiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }
                else
                {
                    ChangePatrolPoint();
                    SetDestination();
                }
            }
            //if we are waiting
            if (_waiting)
            {
                _waitTimer += Time.deltaTime;
                if(_waitTimer >= _totalWaitTime)
                {
                    _waiting = false;

                    ChangePatrolPoint();
                    SetDestination();

                }
            }
        }

        public void SetDestination()
        {
            if(_patrolPoints != null)
            {
                Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
                _navMeshAgent.SetDestination(targetVector);
                _traveling = true;
            }
        }

        //selects new patrol point from list
        //with small probability that makes it turn around

        private void ChangePatrolPoint()
        {
            if(UnityEngine.Random.Range(0f,1f) <= _switchProbability)
            {
                _patrolForward = !_patrolForward;
            }


            if (_patrolForward)
            {
                _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
            }
            else
            {
                if(--_currentPatrolIndex < 0)
                {
                    _currentPatrolIndex = _patrolPoints.Count - 1;
                }
            }
        }


    }
}
