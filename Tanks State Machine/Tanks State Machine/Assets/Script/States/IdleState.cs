using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.States
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "Unity-States/Idle", order = 1)]
     public class IdleState:NewBehaviourScript
    {
        [SerializeField]
        float _idleDuration = 1f;
        float _totalDuration;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState()
        {
           EnteredState = base.EnterState();
            if (EnteredState)
            {
                UnityEngine.Debug.Log("Entered Idle State");
                _totalDuration = 0f;

            }

            

            return EnteredState;
        }
        public override void UpdateState()
        {
            if (EnteredState)
            {
                _totalDuration += Time.deltaTime;
               
              UnityEngine.Debug.Log("Updating Idle State");
                
                if(_totalDuration>= _idleDuration)
                {
                    _fsm.EnterState(FSMStateType.PATROL);

                }

            }
        }


        public override bool ExitState()
        {
            base.ExitState();

            UnityEngine.Debug.Log("Exiting Idle State");

            return true;
        }
    }
}
