using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDatabase : ScriptableObject {

    [SerializeField]
    private PowerupInfoDatabase m_quiExurga;
    [SerializeField]
    private PowerupInfoDatabase m_droceoDrone;
    [SerializeField]
    private PowerupInfoDatabase m_orbRepair;

    public PowerupInfoDatabase GetDatabase(IPowerup.Type type)
    {
        switch (type)
        {
            case IPowerup.Type.Droceo_Drone:
                return m_droceoDrone;
            case IPowerup.Type.Orb_Repair:
                return m_orbRepair;
            case IPowerup.Type.QuiExurga:
                return m_quiExurga;
        }

        return null;
    }
}
