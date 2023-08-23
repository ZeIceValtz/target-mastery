using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField]
    private BaseStand[] m_targetStands;

    public int TargetStandCount => m_targetStands.Length;

    public void Recycle()
    {
        Clear();
        foreach (BaseStand stand in m_targetStands)
        {
            stand.Recycle();
        }
        Destroy(gameObject);
    }

    public void Clear()
    {
        foreach (BaseStand targetStand in m_targetStands)
        {
            targetStand.Clear();
        }
    }

    public bool TryPlace(IStandPlaceable placeable, BaseStand stand)
    {
        return stand.TryPlace(placeable);
    }

    public BaseStand GetTargetStand(int index)
    {
        if (index >= m_targetStands.Length)
            return null;

        return m_targetStands[index];
    }
}
