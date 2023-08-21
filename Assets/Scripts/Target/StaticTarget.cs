using UnityEngine;

public class StaticTarget : BaseTarget
{
    private void Awake()
    {
        if (AnchorPoint == null)
            throw new UnityException("AnchorPoint is not assigned in the inspector.");

        if (AnchorPoint.parent != transform)
            AnchorPoint.SetParent(transform);
    }

    public override void Recycle()
    {
        Destroy(gameObject);
    }
}
