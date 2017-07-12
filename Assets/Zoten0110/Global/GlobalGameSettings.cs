using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameSettings : Singleton<GlobalGameSettings>
{
    [Header("Feature Settings")]
    [SerializeField]
    private bool m_platformGenerator = true;
    [SerializeField]
    private bool m_trashGenerator = true;
    [SerializeField]
    private bool m_obstacleGenerator = true;
    [SerializeField]
    private bool m_powerupGenerator = true;
    [SerializeField]
    private bool m_characterGenerator = true;
    [SerializeField]
    private PowerupDatabase m_powerupDatabase;

    [SerializeField]
    private float m_platformSpeed;

    [SerializeField]
    private float m_trashMissDamage;
    [SerializeField]
    private float m_disposeFailDamage;





    public bool enablePlatformGenerator { get { return m_platformGenerator; } }
    public bool enableTrashGenerator { get { return m_trashGenerator; } }
    public bool enableObstacleGenerator { get { return m_obstacleGenerator; } }
    public bool enablePowerupGenerator { get { return m_powerupGenerator; } }
    public bool enableCharacterGenerator { get { return m_characterGenerator; } }

    public float platformSpeed { get { return m_platformSpeed; } }
    public float trashMissDamage { get { return m_trashMissDamage; } }
    public float disposeFailDamage { get { return m_disposeFailDamage; } }
    public PowerupDatabase powerupDatabase { get { return m_powerupDatabase; } }


    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }

        if (m_instance != this)
        {
            Debug.LogWarning("Duplicate Global Game Settings at " + gameObject.name);
            Destroy(this);
        }

    }



}