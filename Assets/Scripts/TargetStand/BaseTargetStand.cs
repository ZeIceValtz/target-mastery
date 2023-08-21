using UnityEngine;
using System;

public abstract class BaseTargetStand : MonoBehaviour, ITargetStandInfoProvider
{
    public abstract float GetSpeed();
    public abstract Vector3 GetNextDestinationPoint();
    public abstract void Clear();
    public abstract bool TryPlaceTarget(BaseTarget target);
    public abstract void Recycle();
}
