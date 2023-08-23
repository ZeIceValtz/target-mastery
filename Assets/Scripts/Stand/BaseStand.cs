using UnityEngine;

public abstract class BaseStand : MonoBehaviour, IReadonlyStand
{
    private IStandPlaceable m_placeable;

    public abstract float GetSpeed();
    public abstract Vector3 GetNextDestinationPoint();
    protected abstract Vector3 GetPlacementPosition();
    protected abstract void OnRecycle();
    
    public void Clear()
    {
        m_placeable.Remove();
        m_placeable = null;
    }

    public void Recycle()
    {
        OnRecycle();
        Clear();
        Destroy(gameObject);
    }

    public bool TryPlace(IStandPlaceable placeable)
    {
        if (m_placeable != null)
            return false;

        m_placeable = placeable;
        placeable.Place(GetPlacementPosition(), this);
        return true;
    }
}
