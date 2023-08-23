using System.Collections.Generic;
using System;
using UnityEngine;

public class KnifeBehaviour
{
    private Dictionary<Type, BaseKnifeState> m_allStates;
    private BaseKnifeState m_currentState;

    public KnifeBehaviour(Knife knife)
    {
        Rigidbody rigidbody = knife.GetComponent<Rigidbody>();
        m_allStates = new Dictionary<Type, BaseKnifeState>();
        m_allStates.Add(typeof(KnifeState_Idle), new KnifeState_Idle(this, rigidbody));
        m_allStates.Add(typeof(KnifeState_OnStand), new KnifeState_OnStand(this, knife));
        m_allStates.Add(typeof(KnifeState_Stuck), new KnifeState_Stuck(this, rigidbody));
        m_allStates.Add(typeof(KnifeState_Throw), new KnifeState_Throw(this, rigidbody, knife));
        TrySwitchState<KnifeState_Idle>();
    }

    public void Tick() => m_currentState?.Tick();

    public void Idle() => m_currentState?.Idle();

    public void Throw() => m_currentState?.Throw();

    public void OnStandState() => m_currentState?.PlaceOnStand();

    public void Stick() => m_currentState?.Stick();

    public bool TrySwitchState<T>() where T : BaseKnifeState
    {
        if (m_currentState != null && m_currentState.GetType() == typeof(T))
            return false;

        if (m_allStates.TryGetValue(typeof(T), out BaseKnifeState newState))
        {
            m_currentState?.Exit();
            m_currentState = newState;
            m_currentState.Enter();
            return true;
        }

        return false;
    }
}
