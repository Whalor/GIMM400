using Assets.Script.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.FSM
{
   public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField]

        NewBehaviourScript _startingState;

        NewBehaviourScript _currentState;

        [SerializeField]
        List<NewBehaviourScript> _validStates;

        Dictionary<FSMStateType, NewBehaviourScript> _fsmStates;

        public void Awake()
        {
            _currentState = null;

            _fsmStates = new Dictionary<FSMStateType, NewBehaviourScript>();

            NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();
            NPC npc = this.GetComponent<NPC>();
            foreach(NewBehaviourScript state in _validStates)
            {
                state.SetExecutingFSM(this);
                state.SetExecutingNPC(npc);
                state.SetNavMeshAgent(navMeshAgent);
                _fsmStates.Add(state.StateType, state);
            }
        }

        public void Start()
        {
            if (_startingState != null)
            {
                EnterState(_startingState);
            }
        }

        #region STATE MANAGMENT

        public void EnterState(NewBehaviourScript nextState)
        {
            if(nextState == null)
            {
                return;
            }

            if(_currentState != null)
            {
                _currentState.ExitState();
            }

            _currentState = nextState;
            _currentState.EnterState();
        }

        public void Update()
        {
            if(_currentState != null)
            {
                _currentState.UpdateState();
            }
        }

        public void EnterState(FSMStateType stateType)
        {
            if (_fsmStates.ContainsKey(stateType))
            {
                NewBehaviourScript nextState = _fsmStates[stateType];

                _currentState.ExitState();

                EnterState(nextState);
            }
        }


        #endregion

    }
}
