using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupLevel
{
    Level1,
    _Count
}

public class PowerupInfoDatabase : ScriptableObject {

    [SerializeField]
    private int[] m_durationList = new int[(int)PowerupLevel._Count];

    public int GetDuration(PowerupLevel level)
    {
        return m_durationList[(int)level];
    }
}
