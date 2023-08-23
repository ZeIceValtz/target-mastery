using System.Collections.Generic;
using UnityEngine;
using System;

public class DestinationMovement
{
    private readonly Transform m_movable;
    private bool m_isMoving = false;
    private int m_currentDestinationIndex = 0;
    private float m_speed;
    private List<Vector3> m_destinations;

    public DestinationMovement(Transform movable, float speed)
    {
        m_destinations = new List<Vector3>();
        m_movable = movable;
        SetSpeed(speed);
    }

    public event Action OnDestinationReached;
    public bool IsMoving => m_isMoving;
    public float Speed => m_speed;

    public bool TryAddDestination(Vector3 point)
    {
        if (m_destinations.Contains(point))
            return false;

        m_destinations.Add(point);
        return true;
    }

    public bool TryRemoveDestination(Vector3 point) => m_destinations.Remove(point);

    public void ClearDestinations() => m_destinations.Clear();

    public void SetSpeed(float newSpeed)
    {
        if (newSpeed < 0)
            throw new ArgumentOutOfRangeException("newSpeed", "This argument can not be less than zero.");
        m_speed = newSpeed;
    }

    public void Start() => m_isMoving = true;

    public void Stop() => m_isMoving = false;

    public void ResetDestination() => m_currentDestinationIndex = 0;
    public void RandomDestination()
    {
        m_currentDestinationIndex = UnityEngine.Random.Range(0, m_destinations.Count);
    }

    public void Update()
    {
        if (!m_isMoving)
            return;

        if (m_destinations.Count == 0)
            return;

        if (IsAtPoint(m_currentDestinationIndex))
        {
            OnDestinationReached?.Invoke();
            m_currentDestinationIndex = GetNextDestinationIndex(m_currentDestinationIndex);
        }
        else
            MoveToPoint(m_destinations[m_currentDestinationIndex]);
    }

    private int GetNextDestinationIndex(int current)
    {
        current++;

        if (current >= m_destinations.Count)
            return 0;

        return current;
    }

    private void MoveToPoint(Vector3 point)
    {
        m_movable.position = Vector3.MoveTowards(m_movable.position, point, m_speed * Time.deltaTime);
    }

    private bool IsAtPoint(int destinationIndex)
    {
        if (destinationIndex >= m_destinations.Count)
            throw new ArgumentOutOfRangeException();

        return m_movable.position == m_destinations[destinationIndex];
    }
}
