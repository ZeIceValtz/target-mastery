using UnityEngine;

public class StaticStand : BaseStand
{
    [SerializeField]
    private Transform m_attachmentPoint;

    public override Vector3 GetNextDestinationPoint()
    {
        return m_attachmentPoint.position;
    }

    public override float GetSpeed()
    {
        return 0.0f;
    }

    protected override Vector3 GetPlacementPosition()
    {
        return m_attachmentPoint.position;
    }

    protected override void OnRecycle()
    {
        if (m_attachmentPoint.gameObject != gameObject)
            Destroy(m_attachmentPoint);
    }
}
