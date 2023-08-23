using UnityEngine;

public class KnifeState_Stuck : BaseKnifeState
{
    private Rigidbody m_rigidbody;

    public KnifeState_Stuck(KnifeBehaviour context, Rigidbody rigidbody) : base(context)
    {
        m_rigidbody = rigidbody;   
    }

    public override void Enter()
    {
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.isKinematic = true;
    }

    public override void Exit()
    {
        m_rigidbody.isKinematic = false;
    }

    public override void Tick()
    {
        
    }

    public override void Idle()
    {
        Context.TrySwitchState<KnifeState_Idle>();
    }

    public override void PlaceOnStand()
    {
        Context.TrySwitchState<KnifeState_OnStand>();
    }
}
