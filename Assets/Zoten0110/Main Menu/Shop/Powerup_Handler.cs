using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePowerupEvent : GameEvent
{
    public IPowerup.Type type;

    public ActivatePowerupEvent(GameObject sender, IPowerup.Type type) : base(sender)
    {
        this.sender = sender;
        this.type = type;
    }
}

public class Powerup_Handler : MonoBehaviour
{

    [SerializeField]
    private GameObject m_orbRepair;
    [SerializeField]
    private Vector3 m_orbRepairInstance;

    [SerializeField]
    private GameObject m_droceoDrone;
    [SerializeField]
    private Vector3 m_droceoDroneInstance;

    [SerializeField]
    private Animator m_quiExurgaAnim;
    [SerializeField]
    private QuiExurga m_quiExurga;

    private void OnOrbRepair()
    {
       var instance =  Instantiate(m_orbRepair, m_orbRepairInstance, Quaternion.identity) as GameObject;
    }

    private void OnDroceoDrone()
    {
        var instance = Instantiate(m_droceoDrone, m_droceoDroneInstance, Quaternion.identity) as GameObject;
    }

    private void OnQuiExurga()
    {
        m_quiExurgaAnim.SetBool("Qui Exurga",true);
        m_quiExurga.Activate();
    }

    private void OnActivatePowerupEvent (ActivatePowerupEvent e)
    {
        switch (e.type)
        {
            case IPowerup.Type.Droceo_Drone:
                OnDroceoDrone();
                break;
            case IPowerup.Type.Orb_Repair:
                OnOrbRepair();
                break;
            case IPowerup.Type.QuiExurga:
                OnQuiExurga();
                break;
        }
    }

    private void OnEnable()
    {
        this.AddGameEventListenerGlobal<ActivatePowerupEvent>(OnActivatePowerupEvent);
    }

    private void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<ActivatePowerupEvent>(OnActivatePowerupEvent);
    }
}
