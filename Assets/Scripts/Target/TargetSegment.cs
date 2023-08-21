using UnityEngine;
using System;

public class TargetSegment : MonoBehaviour, IHittable //Idamageable
{
    [SerializeField]
    private int m_hitValue = 1;

    public int HitValue => m_hitValue;
    public event Action<TargetSegment> OnHit;

    public void Hit()
    {
        OnHit?.Invoke(this);
    }

    private void OnValidate()
    {
        if (m_hitValue < 0)
        {
            m_hitValue = 0;
            throw new UnityException("HitValue can not be negative.");
        }
    }
}
