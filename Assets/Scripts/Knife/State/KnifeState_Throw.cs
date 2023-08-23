using UnityEngine;

public class KnifeState_Throw : BaseKnifeState
{
    private Rigidbody m_rigidbody;
    private Transform m_transform;
    private Knife m_knife;

    public KnifeState_Throw(KnifeBehaviour context, Rigidbody rigidbody, Knife knife) : base(context)
    {
        m_rigidbody = rigidbody;
        m_knife = knife;
        m_transform = knife.transform;
    }

    public override void Enter()
    {
        Vector3 force = m_knife.ThrowDirection * m_knife.ThrowForce;
        m_rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public override void Exit()
    {
        m_rigidbody.velocity = Vector3.zero;
    }

    public override void Tick()
    {
        Ray hitScanRay = GetHitScanRay();

        if (!Physics.Raycast(hitScanRay, m_knife.HitRayLength))
            return;

        IDamageable damageable = GetComponentFromRaycast<IDamageable>(hitScanRay, m_knife.HitRayLength);
        IHittable hittable = GetComponentFromRaycast<IHittable>(hitScanRay, m_knife.HitRayLength);
        IStickableSurface sticable = GetComponentFromRaycast<IStickableSurface>(hitScanRay, m_knife.HitRayLength);

        if (damageable != null)
            damageable.Damage(m_knife.Damage);

        if (hittable != null)
            hittable.Hit();

        if (sticable == null)
            Context.TrySwitchState<KnifeState_Idle>();
        else
            Context.TrySwitchState<KnifeState_Stuck>();
    }

    private Ray GetHitScanRay()
    {
        return new Ray(m_transform.position + m_knife.ThrowDirection * m_knife.HitRayOffsetY, m_knife.ThrowDirection);
    }

    private T GetComponentFromRaycast<T>(Ray ray, float rayDistance)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (rayDistance < Vector3.Distance(hit.point, ray.origin))
                return default;

            T component = hit.collider.GetComponent<T>();
            return component;
        }
        return default;
    }
}
