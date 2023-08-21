using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField]
    private BaseTargetStand[] m_targetStands;

    public int TargetStandCount => m_targetStands.Length;

    public void Recycle()
    {
        Clear();
        foreach (BaseTargetStand stand in m_targetStands)
        {
            stand.Recycle();
        }
        Destroy(gameObject);
    }

    public void Clear()
    {
        foreach (BaseTargetStand targetStand in m_targetStands)
        {
            targetStand.Clear();
        }
    }

    public bool TryPlaceTarget(BaseTarget target, BaseTargetStand targetStand)
    {
        return targetStand.TryPlaceTarget(target);
    }

    public BaseTargetStand GetTargetStand(int index)
    {
        if (index >= m_targetStands.Length)
            return null;

        return m_targetStands[index];
    }
}
