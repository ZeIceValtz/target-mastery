using UnityEngine;
using System;

public abstract class BaseTarget : MonoBehaviour, IStandPlaceable, IScoreAdder
{
    [SerializeField]
    private Transform m_standAttachmentPoint; // make at a Vector3 m_standOffset
    [SerializeField]
    private TargetSegment[] m_targetSegments;

    protected Transform StandAttachmentPoint => m_standAttachmentPoint;
    protected TargetSegment[] TargetSegments => m_targetSegments;
    protected IReadonlyStand TargetStandInfo { get; private set; }

    public event Action<int> OnAddScore;

    private bool m_isInitialized = false;

    public void Place(Vector3 position, IReadonlyStand standInfo)
    {
        if (!m_isInitialized)
            throw new Exception("Target is not initialized. You must call Initialize function, before placing it.");

        transform.position = position - StandAttachmentPoint.localPosition;
        TargetStandInfo = standInfo;
        OnTargetPlaced();
    }

    public void Remove()
    {
        TargetStandInfo = null;
        OnTargetRemoved();
    }

    public void Initialize()
    {
        foreach (TargetSegment segment in m_targetSegments)
        {
            segment.OnHit += HandleTargetHit;
        }
        OnInitialize();
        m_isInitialized = true;
    }

    public void Recycle()
    {
        OnRecycle();
        foreach (TargetSegment segment in m_targetSegments)
        {
            segment.OnHit -= HandleTargetHit;
            if (segment.gameObject != gameObject)
                Destroy(segment.gameObject);
        }
        Destroy(m_standAttachmentPoint);
        Destroy(gameObject);
    }

    protected abstract void OnInitialize();
    protected abstract void OnRecycle();
    protected virtual void OnTargetHit() { }
    protected virtual void OnTargetPlaced() { }
    protected virtual void OnTargetRemoved() { }

    private void HandleTargetHit(TargetSegment segment)
    {
        int scrore = segment.HitValue;
        OnAddScore?.Invoke(scrore);
        OnTargetHit();
    }
}
