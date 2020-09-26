using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.States
{
    [CreateAssetMenu(fileName = "ChaseState", menuName = "Unity-States/Chase", order = 3)]
    public class ChaseState : NewBehaviourScript
    {
        private Transform playerPos;
        float playerDist = 30.0f;
        float viewAngle = 40.0f;
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.CHASE;
        }

        public override bool EnterState()
        {
            EnteredState = base.EnterState();
            if (EnteredState)
            {
                UnityEngine.Debug.Log("Entered Chase State");

            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                UnityEngine.Debug.Log("Chasing");
                playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                Vector3 direction = playerPos.position - _navMeshAgent.transform.position;
                float angle = Vector3.Angle(direction, _navMeshAgent.transform.forward);
                
                //make sure player is still close
                if (direction.magnitude > playerDist)
                {
                    //if not still close, return to patrol
                    _fsm.EnterState(FSMStateType.PATROL);
                } else
                {
                    //orient towards player
                    direction.y = 0;
                    _navMeshAgent.transform.rotation = Quaternion.Slerp(_navMeshAgent.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 1);

                    _navMeshAgent.SetDestination(playerPos.position);
                }
            }

        }

        public override bool ExitState()
        {
            base.ExitState();

            UnityEngine.Debug.Log("Exiting Chase State");

            return true;
        }
    }
}
