using UnityEngine;

[CreateAssetMenu]
public class TargetFactory : ScriptableObject
{
    [SerializeField]
    private StaticTarget m_staticTargetPrefab;
    [SerializeField]
    private MovingTarget m_movingTargetPrefab;

    public BaseTarget CreateTarget(TargetType targetType)
    {
        switch (targetType)
        {
            case TargetType.Static:
                return Spawn(m_staticTargetPrefab);
            case TargetType.Moving:
                return Spawn(m_movingTargetPrefab);
        }

        return null;
    }

    private T Spawn<T>(T prefab) where T : BaseTarget
    {
        T instance = GameObject.Instantiate(prefab);
        return instance;
    }
}

public enum TargetType
{
    Static,
    Moving
}
