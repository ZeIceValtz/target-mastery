using UnityEngine;

public class StaticStand : BaseTargetStand
{
    [SerializeField]
    private Transform m_targetAttachmentPoint;

    public override Vector3 GetNextDestinationPoint()
    {
        return m_targetAttachmentPoint.position;
    }

    public override float GetSpeed()
    {
        return 0.0f;
    }

    protected override Vector3 GetTargetPlacement()
    {
        return m_targetAttachmentPoint.position;
    }

    protected override void OnRecycle()
    {
        if (m_targetAttachmentPoint.gameObject != gameObject)
            Destroy(m_targetAttachmentPoint);
    }
}
