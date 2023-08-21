using UnityEngine;
using System;

public abstract class BaseTarget : MonoBehaviour, IScoreAdder
{
    [SerializeField]
    private Transform m_anchorPoint;
    [SerializeField]
    private TargetSegment[] m_targetSegments;

    protected Transform AnchorPoint => m_anchorPoint;
    protected TargetSegment[] TargetSegments => m_targetSegments;
    protected ITargetStandInfoProvider TargetStandInfo { get; private set; }

    public event Action<int> OnAddScore;

    public void Initialize()
    {
        foreach (TargetSegment segment in m_targetSegments)
        {
            segment.OnHit += HandleTargetHit;
        }
        OnInitialize();
    }

    public void PlaceSelf(Vector3 position, ITargetStandInfoProvider standInfo)
    {
        transform.position = position - AnchorPoint.localPosition;
        TargetStandInfo = standInfo;
        OnTargetPlaced();
    }

    public void Recycle()
    {
        OnRecycle();
        foreach (TargetSegment segment in m_targetSegments)
        {
            segment.OnHit -= HandleTargetHit;
            Destroy(segment.gameObject);
        }
        Destroy(m_anchorPoint);
        Destroy(gameObject);
    }

    protected abstract void OnInitialize();
    protected abstract void OnRecycle();
    protected virtual void OnTargetHit() { }
    protected virtual void OnTargetPlaced() { }

    private void HandleTargetHit(TargetSegment segment)
    {
        int scrore = segment.HitValue;
        OnAddScore?.Invoke(scrore);
        OnTargetHit();
    }
}
