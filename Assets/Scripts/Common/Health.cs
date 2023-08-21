using UnityEngine;
using System;

public class Health
{
    private int m_maxHealth;
    private int m_currentHealth;

    public Health(int maxHealth, int currentHealth)
    {
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
    }

    public event Action OnHealthChanged;
    public event Action OnDied;

    public int MaxHealth => m_maxHealth;
    public int CurrentHealth => m_currentHealth;

    public void Modify(int amount)
    {
        if (amount == 0)
            return;

        SetHealth(Mathf.Clamp(m_currentHealth + amount, 0, m_maxHealth));
    }

    public void SetMaxHealth(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException("Can not be set to less than zero.");

        m_maxHealth = value;
        SetHealth(Mathf.Clamp(m_currentHealth, 0, m_maxHealth));
    }

    public void SetHealth(int value)
    {
        if (value < 0 || value > m_maxHealth)
            throw new ArgumentOutOfRangeException("Must be in the range between zero and MaxHealth.");

        if (m_currentHealth == value)
            return;

        m_currentHealth = value;
        OnHealthChanged?.Invoke();

        if (m_currentHealth == 0)
            OnDied?.Invoke();
    }
}
