using UnityEngine;

public abstract class BaseTarget : MonoBehaviour
{
    [field: SerializeField]
    protected Transform AnchorPoint { get; private set; }

    protected ITargetStandInfoProvider TargetStand { get; private set; }

    public virtual void PlaceSelf(Vector3 position, ITargetStandInfoProvider targetStand)
    {
        transform.position = position - AnchorPoint.localPosition;
        TargetStand = targetStand;
    }

    public abstract void Recycle();
}
