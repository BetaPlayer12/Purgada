using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictDiseaseToPlayerEvent : GameEvent
{
    
    public DiseaseType diseaseType;

    public InflictDiseaseToPlayerEvent(GameObject senderObj, DiseaseType type) : base(senderObj)
    {
        sender = senderObj;
        diseaseType = type;
    }
}

public enum DiseaseType
{
    Nausea,
    Tetanus,
    Repiratory
}

public class DiseaseHandler : MonoBehaviour {

    [SerializeField]
    private Disease_Nausea m_nausea;
    [SerializeField]
    private Disease_Respiratory m_respiratory;
    [SerializeField]
    private Disease_Tetanus m_tetanus;

    private void OnInflictDiseaseToPlayerEvent(InflictDiseaseToPlayerEvent e)
    {
        switch (e.diseaseType)
        {
            case DiseaseType.Nausea:
                m_nausea.Inflict();
                break;
            case DiseaseType.Repiratory:
                m_respiratory.Inflict();
                break;
            case DiseaseType.Tetanus:
                m_tetanus.Inflict();
                break;
        }

        Debug.Log($"{e.diseaseType} is Inflicted");
    }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<InflictDiseaseToPlayerEvent>(OnInflictDiseaseToPlayerEvent);
    }

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<InflictDiseaseToPlayerEvent>(OnInflictDiseaseToPlayerEvent);
    }

}
