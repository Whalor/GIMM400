using Assets.Script.FSM;
using Assets.Script.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED, 
    TERMINATED,
};

public enum FSMStateType
{
    IDLE,
    PATROL,
    CHASE,
}


public abstract class NewBehaviourScript : ScriptableObject 
{

    protected NavMeshAgent _navMeshAgent;
    protected NPC _npc;
    protected FiniteStateMachine _fsm;

    public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }
    public bool EnteredState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

   public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successNPC = true;

        ExecutionState = ExecutionState.ACTIVE;

        //does navmesh agent exist?
        successNavMesh = (_navMeshAgent != null);

        //does Execute agent exist?
        successNPC = (_npc !=null);

        return successNPC & successNavMesh;
    }

    public abstract void UpdateState();



    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        if(navMeshAgent != null)
        {
            _navMeshAgent = navMeshAgent;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine fsm)
    {
        if(fsm != null)
        {
            _fsm = fsm;
        }
    }

    public virtual void SetExecutingNPC(NPC npc)
    {
        if(npc != null)
        {
            _npc = npc;
        }
    }

}
