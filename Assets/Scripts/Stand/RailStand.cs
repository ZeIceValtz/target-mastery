using System.Collections.Generic;
using UnityEngine;

public class RailStand : BaseStand
{
    [SerializeField]
    private Transform[] m_railPoints;
    [SerializeField]
    private bool m_isLooped;
    [SerializeField] [Range(1, 10)]
    private float m_targetSpeed = 5f;

    private int m_currentDirection = 1;
    private int m_currentDestinationIndex;

    private void Awake()
    {
        if (m_railPoints.Length < 2)
            throw new UnityException("RailStand must have at least two Rail Points.");
    }

    public override Vector3 GetNextDestinationPoint()
    {
        Vector3[] railPoints = GetRailPoints();
        m_currentDestinationIndex = GetNextIndex(m_currentDestinationIndex, railPoints.Length, m_currentDirection, m_isLooped);

        // changing direction if hit a deadlock
        if (!m_isLooped)
        {
            if (m_currentDestinationIndex == 0)
                m_currentDirection = 1;
            else if (m_currentDestinationIndex == railPoints.Length - 1)
                m_currentDirection = -1;
        }

        return railPoints[m_currentDestinationIndex];
    }

    public override float GetSpeed()
    {
        return m_targetSpeed;
    }

    protected override Vector3 GetPlacementPosition()
    {
        Vector3[] railPoints = GetRailPoints();
        int randomPointIndex = Random.Range(0, railPoints.Length);
        int nearbyPointIndex = GetNextIndex(randomPointIndex, railPoints.Length, 1, m_isLooped);
        Vector3 randomRailPosition = RandomPositionBetween(railPoints[randomPointIndex], railPoints[nearbyPointIndex]);
        m_currentDestinationIndex = randomPointIndex;
        return randomRailPosition;
    }

    protected override void OnRecycle()
    {
        foreach (Transform point in m_railPoints)
        {
            if (point.gameObject != gameObject)
                Destroy(point);
        }
    }

    private Vector3[] GetRailPoints()
    {
        List<Vector3> railPoints = new List<Vector3>();
        foreach (Transform railPointTransform in m_railPoints)
        {
            railPoints.Add(railPointTransform.position);
        }
        return railPoints.ToArray();
    }

    private Vector3 RandomPositionBetween(Vector3 a, Vector3 b)
    {
        float distanceMultiplier = Random.Range(0f, 1f);
        return Vector3.Lerp(a, b, distanceMultiplier);
    }

    private int GetNextIndex(int current, int length, int direction, bool isLooped)
    {
        if (current < 0 || length < 0)
            throw new System.ArgumentOutOfRangeException("current, length", "Arguments must be of positive value.");

        if (Mathf.Abs(direction) != 1)
            throw new System.ArgumentOutOfRangeException("direction", "Direction can only be euqal to 1 or -1.");

        if (isLooped)
            return (int)Mathf.Repeat(current + direction, length);

        if (current + direction == length)
            return current - 1;
        else if (current + direction == -1)
            return current + 1;

        return current + direction;
    }
}