using UnityEngine;

public abstract class BaseTargetStand : MonoBehaviour, ITargetStandInfoProvider
{
    private BaseTarget m_target;

    public BaseTarget Target => m_target;

    public abstract float GetSpeed();
    public abstract Vector3 GetNextDestinationPoint();
    protected abstract Vector3 GetTargetPlacement();
    protected abstract void OnRecycle();
    protected virtual void OnClear() { }
    protected virtual void OnTargetPlace() { }
    
    public void Clear()
    {
        OnClear();
        m_target.Recycle();
        m_target = null;
    }

    public void Recycle()
    {
        OnRecycle();
        Clear();
        Destroy(gameObject);
    }

    public bool TryPlaceTarget(BaseTarget target)
    {
        if (m_target != null)
            return false;

        OnTargetPlace();
        m_target = target;
        target.Initialize();
        target.PlaceSelf(GetTargetPlacement(), this);
        return true;
    }
}
