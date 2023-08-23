using UnityEngine;

public class GameFieldGenerator : MonoBehaviour
{
    [SerializeField]
    private GameField m_gameField;
    [SerializeField]
    private TargetFactory m_targetFactory;
    [SerializeField]
    private Knife m_knife;

    private void Awake()
    {
        BaseStand targetStand = m_gameField.GetTargetStand(0);
        BaseTarget target = m_targetFactory.CreateTarget(TargetType.Moving);
        target.Initialize();
        //m_gameField.TryPlace(target, targetStand);

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            m_gameField.TryPlace(m_knife, m_gameField.GetTargetStand(0));
    }

    
}
