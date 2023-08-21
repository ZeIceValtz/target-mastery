using System.Collections.Generic;
using UnityEngine;

public class RailStand : BaseTargetStand
{
    [SerializeField]
    private Transform[] m_railPoints;
    [SerializeField]
    private bool m_isLooped = false;

    private int m_currentDestinationIndex = 0;
    private int m_direction = 1;
    private BaseTarget m_target;

    private void Awake()
    {
        foreach(Transform railPoint in m_railPoints)
        {
            if (railPoint.parent != transform)
                railPoint.SetParent(transform);
        }

        if (Random.Range(0f, 1f) > 0.5f)
            m_direction = 1;
        else
            m_direction = -1;
    }

    public override float GetSpeed()
    {
        return 5;
    }

    public override Vector3 GetNextDestinationPoint()
    {
        Vector3[] railPoints = GetRailPoints();
        m_currentDestinationIndex = GetNextPointIndex(m_currentDestinationIndex, railPoints.Length, m_isLooped);
        return railPoints[m_currentDestinationIndex];
    }

    public override void Clear()
    {
        m_target?.Recycle();
        m_target = null;
    }

    public override bool TryPlaceTarget(BaseTarget target)
    {
        if (m_target != null)
            return false;

        m_target = target;

        Vector3[] railPoints = GetRailPoints();

        // get random position on a random rail to spawn target there
        int randomPointIndex = Random.Range(0, railPoints.Length);
        int nearbyPointIndex = GetNextPointIndex(randomPointIndex, railPoints.Length, m_isLooped);
        Vector3 randomEndPoint = railPoints[randomPointIndex];
        Vector3 nearbyEndPoint = railPoints[nearbyPointIndex];
        Vector3 randomPosition = RandomPositionBetween(randomEndPoint, nearbyEndPoint);
        target.PlaceSelf(randomPosition, this);

        m_currentDestinationIndex = randomPointIndex;
        return true;
    }

    public override void Recycle()
    {
        Clear();
        Destroy(gameObject);
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

    private int GetNextPointIndex(int current, int length, bool isLooped)
    {
        if (isLooped)
            return (int)Mathf.Repeat(current + m_direction, length);

        if (current + m_direction == length || current + m_direction < 0)
            m_direction *= -1;

        return current + m_direction;
    }

    private Vector3 RandomPositionBetween(Vector3 a, Vector3 b)
    {
        float distanceMultiplier = Random.Range(0f, 1f);
        return Vector3.Lerp(a, b, distanceMultiplier);
    }
}
