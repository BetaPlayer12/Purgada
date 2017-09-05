using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterMovementSensor : MonoBehaviour {

    public EncounterMovement.Direction m_shiftTo;
    public EncounterMovement m_movementModule;
    public GameObject m_floor;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == m_floor)
        {
            m_movementModule.direction = m_shiftTo;
        }
    }
}
