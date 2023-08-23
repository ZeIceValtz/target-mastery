using UnityEngine;

public class KnifeState_OnStand : BaseKnifeState
{
    private Transform m_transform;
    private Rigidbody m_rigidbody;
    private Knife m_knife;
    private DestinationMovement m_movement;

    public KnifeState_OnStand(KnifeBehaviour context, Knife knife) : base(context)
    {
        m_knife = knife;
        m_transform = knife.transform;
        m_rigidbody = knife.GetComponent<Rigidbody>();
        m_movement = new DestinationMovement(m_transform, 0.0f);
    }

    public override void Enter()
    {
        if (m_knife.Stand == null)
            throw new System.NullReferenceException("You can not enter the state if BaseStand is null in your Knife class.");

        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.isKinematic = true;
        m_movement.OnDestinationReached += HandleDestinationReached;
        HandleDestinationReached();
    }

    public override void Exit()
    {
        m_movement.OnDestinationReached -= HandleDestinationReached;
        m_rigidbody.isKinematic = false;
    }

    public override void Tick()
    {
        m_movement.Update();
    }

    public override void Idle()
    {
        Context.TrySwitchState<KnifeState_Idle>();
    }

    public override void Throw()
    {
        Context.TrySwitchState<KnifeState_Throw>();
    }

    private void HandleDestinationReached()
    {
        m_movement.Stop();
        m_movement.ClearDestinations();
        m_movement.TryAddDestination(m_knife.Stand.GetNextDestinationPoint() + m_knife.StandOffset);
        m_movement.SetSpeed(m_knife.Stand.GetSpeed());
        m_movement.Start();
    }
}