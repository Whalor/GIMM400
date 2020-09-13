using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEngine;

namespace Assets.Script.States
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-States/Patrol", order = 2)]

    public class PatrolState : NewBehaviourScript
    {
        WayPoints[] _patrolPoints;
        int _patrolPointIndex;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.PATROL;
            _patrolPointIndex = -1;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                //grab and store patrol points 
                _patrolPoints = _npc.PatrolPoints;
                if(_patrolPoints == null || _patrolPoints.Length == 0)
                {
                    Debug.LogError("patrolstate failed to grab waypoints from NPC");
                    
                }
                else 
                { 
                    if(_patrolPointIndex < 0)
                    {
                        _patrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);

                    }
                    else
                    {
                        _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
                    }
                      SetDestination(_patrolPoints[_patrolPointIndex]);
                      EnteredState = true;

                }

            } 
                 return EnteredState;
        }
        
        public override void UpdateState()
        {
            //need to make sure entered state
            if (EnteredState)
            {
                //logic
                if(Vector3.Distance(_navMeshAgent.transform.position, _patrolPoints[_patrolPointIndex].transform.position) <= 1f)
                {
                    _fsm.EnterState(FSMStateType.IDLE);
                }
            }
        }

        private void SetDestination(WayPoints destination)
        {
            if(_navMeshAgent != null && destination != null)
            {
                _navMeshAgent.SetDestination(destination.transform.position);
            }
        }
    }
}
