using Assets.Script.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.States
{

    [RequireComponent(typeof(NavMeshAgent) ,typeof(FiniteStateMachine))]
    public class NPC : MonoBehaviour
    { 
        
        [SerializeField]
        WayPoints[] _patrolPoints;

        NavMeshAgent _navMeshAgent;
        FiniteStateMachine _finiteStateMachine;

       

        public void Awake()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        public WayPoints[] PatrolPoints
        {
            get
            {
                return _patrolPoints;
            }
        }
    }
}
