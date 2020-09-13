using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.States
{
    class ConnectedWaypoints : WayPoints
    {

        [SerializeField]
        protected float _connectivityRadius = 37f;

        List<ConnectedWaypoints> _connections;

        public void Start()
        {
            //get all waypoints in scene
            GameObject[] allWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");

            //create list
            _connections = new List<ConnectedWaypoints>();

            //check if connected
            for(int i = 0; i < allWayPoints.Length; i++)
            {
                ConnectedWaypoints nextWaypoint = allWayPoints[i].GetComponent<ConnectedWaypoints>();

                //found waypoint?
                if(nextWaypoint != null)
                {
                    if(Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= _connectivityRadius && nextWaypoint != this)
                    {
                        _connections.Add(nextWaypoint);
                    }
                }
            }
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _connectivityRadius);
        }

        public ConnectedWaypoints NextWaypoint(ConnectedWaypoints previousWaypoint)
        {
            if(_connections.Count == 0)
            {
                //no waypoint? return null
                Debug.LogError("no waypoints count.");
                return null;
            }
            else if(_connections.Count == 1 && _connections.Contains(previousWaypoint))
            {
                //only 1 waypoint and its where we just were? use it
                return previousWaypoint;
            }
            else // or else find a random one that isnt the last one we were at
            {
                ConnectedWaypoints nextWaypoint;
                int nextIndex = 0;

                do
                {
                    nextIndex = UnityEngine.Random.Range(0, _connections.Count);
                    nextWaypoint = _connections[nextIndex];
                } while (nextWaypoint == previousWaypoint);

                return nextWaypoint;
            }
        }

    }
}
