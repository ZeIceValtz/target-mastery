using UnityEngine;

public class GameFieldGenerator : MonoBehaviour
{
    [SerializeField]
    private GameField m_gameField;
    [SerializeField]
    private TargetFactory m_targetFactory;

    private void Awake()
    {
        BaseTargetStand targetStand = m_gameField.GetTargetStand(0);
        BaseTargetStand targetStand1 = m_gameField.GetTargetStand(1);
        m_gameField.TryPlaceTarget(m_targetFactory.CreateTarget(TargetType.Moving), targetStand);
        m_gameField.TryPlaceTarget(m_targetFactory.CreateTarget(TargetType.Moving), targetStand1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            m_gameField.Clear();
    }

    
}
