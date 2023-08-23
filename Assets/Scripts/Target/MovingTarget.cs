public class MovingTarget : BaseTarget
{
    private DestinationMovement m_movement;

    private void Awake()
    {
        if (StandAttachmentPoint.gameObject != gameObject && StandAttachmentPoint.parent != transform)
            StandAttachmentPoint.SetParent(transform);
    }

    private void Update()
    {
        m_movement.Update();
    }

    protected override void OnInitialize()
    {
        m_movement = new DestinationMovement(transform, 0f);
        m_movement.OnDestinationReached += MoveToNextPoint;
    }

    protected override void OnRecycle()
    {
        m_movement.OnDestinationReached -= MoveToNextPoint;
    }

    protected override void OnTargetPlaced()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        m_movement.Stop();
        m_movement.ClearDestinations();
        m_movement.TryAddDestination(TargetStandInfo.GetNextDestinationPoint() - StandAttachmentPoint.localPosition);
        m_movement.SetSpeed(TargetStandInfo.GetSpeed());
        m_movement.Start();
    }
}
