using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.States
{
   public class EnemyNewPatrol : MonoBehaviour
    {

        //sees if we wait in each node
        [SerializeField]
        bool _patrolWaiting;

        //how long we wait
        [SerializeField]
        float _totalWaitTime = 3f;

        //probability of change direction
        [SerializeField]
        float _switchProbability = 0.2f;

        //variables for base behavior
        NavMeshAgent _navMeshAgent;
        ConnectedWaypoints _currentWaypoint;
        ConnectedWaypoints _previousWaypoint;

        bool _traveling;
        bool _waiting;
        float _waitTimer;
        int _waypointVisited;

        //initilization 
        public void Start()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();

            if(_navMeshAgent == null)
            {
                Debug.LogError("the nav mesh agent is not attached to " + gameObject.name);

            }
            else
            {
                if(_currentWaypoint == null)
                {
                //set renadom
                //grab all waypoint objects
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                    if(allWaypoints.Length > 0)
                    {
                        while(_currentWaypoint == null)
                        {
                            int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                            ConnectedWaypoints startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoints>();

                            //i.e. we found waypoint
                            if(startingWaypoint != null)
                            {
                                _currentWaypoint = startingWaypoint;

                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("failed to find any waypoints to use in the scene");
                    }


                }
                SetDestination();
               
            }

        }

        public void Update()
        {
            //check if close to dest.
            if(_traveling && _navMeshAgent.remainingDistance <= 1.0f)
            {
                _traveling = false;
                _waypointVisited++;

                //if going to wait, wait
                if (_patrolWaiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }
                else
                {
                    SetDestination();
                }
            }

            //if waiting
            if (_waiting)
            {
                _waitTimer += Time.deltaTime;
                if(_waitTimer >= _totalWaitTime)
                {
                    _waiting = false;

                    SetDestination();
                }
            }
        }

        private void SetDestination()
        {
            if(_waypointVisited > 0)
            {
                //find next waypoint                                
                ConnectedWaypoints nextWayponit = _currentWaypoint.NextWaypoint(_previousWaypoint);
                _previousWaypoint = _currentWaypoint;
                _currentWaypoint = nextWayponit;
            }

            Vector3 targetVector = _currentWaypoint.transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _traveling = true;
        }

    }

    
}
