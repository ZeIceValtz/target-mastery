using UnityEngine;

public class Knife : MonoBehaviour, IStandPlaceable
{
    [SerializeField]
    private int m_damage = 1;
    [SerializeField]
    private float m_throwForce = 5f;
    [SerializeField]
    private float m_hitRayOffsetZ = 0.5f;
    [SerializeField]
    private float m_hitRayLength = 0.3f;
    [SerializeField]
    private Vector3 m_standOffset;

    public int Damage => m_damage;
    public float ThrowForce => m_throwForce;
    public float HitRayOffsetY => m_hitRayOffsetZ;
    public float HitRayLength => m_hitRayLength;
    public Vector3 StandOffset => m_standOffset;
    public Vector3 ThrowDirection => transform.forward;
    public IReadonlyStand Stand => m_stand;

    private KnifeBehaviour m_behaviour;
    private IReadonlyStand m_stand;

    private void Awake()
    {
        m_behaviour = new KnifeBehaviour(this);
    }

    private void Update()
    {
        m_behaviour.Tick();

        if (Input.GetKeyDown(KeyCode.A))
            Throw();
    }

    public void Place(Vector3 position, IReadonlyStand stand)
    {
        m_stand = stand;
        transform.position = position + m_standOffset;
        transform.rotation = Quaternion.identity;
        m_behaviour.OnStandState();
    }

    public void Remove()
    {
        m_stand = null;
        m_behaviour.Idle();
    }

    public void Throw()
    {
        m_behaviour.Throw();
    }
}
