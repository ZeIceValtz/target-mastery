using UnityEngine;

public class KnifeState_Idle : BaseKnifeState
{
    private Rigidbody m_rigidbody;

    public KnifeState_Idle(KnifeBehaviour context, Rigidbody rigidbody) : base(context)
    {
        m_rigidbody = rigidbody;
    }

    public override void Enter()
    {
        m_rigidbody.useGravity = true;
    }

    public override void Exit()
    {
        m_rigidbody.useGravity = false;
    }

    public override void Tick()
    {

    }

    public override void PlaceOnStand()
    {
        Context.TrySwitchState<KnifeState_OnStand>();
    }

    public override void Stick()
    {
        Context.TrySwitchState<KnifeState_Stuck>();
    }
}