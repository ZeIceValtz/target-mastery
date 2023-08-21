using UnityEngine;
using System;

public class TargetSegment : MonoBehaviour, IHittable, IDamageable
{
    [SerializeField]
    private int m_hitValue = 1;

    public int HitValue => m_hitValue;
    public event Action<TargetSegment> OnHit;
    public event Action<int> OnDamageReceived;

    public void Damage(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException("amount", "Damage amount can not be negative.");
        OnDamageReceived?.Invoke(amount);
    }

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
