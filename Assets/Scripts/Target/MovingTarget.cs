using UnityEngine;

public class MovingTarget : BaseTarget
{
    private DestinationMovement m_movement;

    private void Awake()
    {
        if (AnchorPoint == null)
            throw new UnityException("AnchorPoint is not assigned in the inspector.");

        if (AnchorPoint.parent != transform)
            AnchorPoint.SetParent(transform);

        m_movement = new DestinationMovement(transform, 0f);
        m_movement.OnDestinationReached += MoveToNextDestination;
    }

    private void Update()
    {
        m_movement.Update();
    }

    public override void PlaceSelf(Vector3 position, ITargetStandInfoProvider targetStand)
    {
        base.PlaceSelf(position, targetStand);
        m_movement.TryAddDestination(targetStand.GetNextDestinationPoint() - AnchorPoint.localPosition);
        m_movement.SetSpeed(targetStand.GetSpeed());
        m_movement.Start();
    }

    public override void Recycle()
    {
        m_movement.OnDestinationReached -= MoveToNextDestination;
        Destroy(gameObject);
    }

    private void MoveToNextDestination()
    {
        m_movement.Stop();
        m_movement.ClearDestinations();
        m_movement.TryAddDestination(TargetStand.GetNextDestinationPoint() - AnchorPoint.localPosition);
        m_movement.Start();
    }
}
