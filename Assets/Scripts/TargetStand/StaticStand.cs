using UnityEngine;

public class StaticStand : BaseTargetStand
{
    [SerializeField]
    private Transform m_targetAttachmentPoint;

    private BaseTarget m_target;

    public override float GetSpeed()
    {
        return 0;
    }

    public override Vector3 GetNextDestinationPoint()
    {
        return m_targetAttachmentPoint.position;
    }

    public override void Clear()
    {
        m_target?.Recycle();
        m_target = null;
    }

    public override void Recycle()
    {
        Clear();
        Destroy(gameObject);
    }

    public override bool TryPlaceTarget(BaseTarget target)
    {
        if (m_target != null)
            return false;

        m_target = target;
        target.PlaceSelf(m_targetAttachmentPoint.position, this);
        return true;
    }
}
