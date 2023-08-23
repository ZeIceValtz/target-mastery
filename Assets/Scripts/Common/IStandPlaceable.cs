using UnityEngine;

public interface IStandPlaceable
{
    void Place(Vector3 position, IReadonlyStand stand);
    void Remove();
}
