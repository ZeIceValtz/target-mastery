using UnityEngine;

public interface ITargetStandInfoProvider
{
    float GetSpeed();
    Vector3 GetNextDestinationPoint();
}