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
    private float m_platformSpeed;

    [SerializeField]
    private float m_trashMissDamage;
    [SerializeField]
    private float m_disposeFailDamage;



    #region StaticFields
    private static bool m_staticPlatformGenerator;
    private static bool m_staticTrashGenerator;
    private static bool m_staticObstacleGenerator;
    private static bool m_staticPowerupGenerator;
    private static bool m_staticCharacterGenerator;

    private static float m_staticPlatformSpeed;
    private static float m_staticTrashMisDamage;
    private static float m_staticDisposeFailDamage;
    #endregion

    public static bool enablePlatformGenerator { get { return m_staticPlatformGenerator; } }
    public static bool enableTrashGenerator { get { return m_staticTrashGenerator; } }
    public static bool enableObstacleGenerator { get { return m_staticObstacleGenerator; } }
    public static bool enablePowerupGenerator { get { return m_staticPowerupGenerator; } }
    public static bool enableCharacterGenerator { get { return m_staticCharacterGenerator; } }

    public static float platformSpeed { get { return m_staticPlatformSpeed; } }
    public static float trashMissDamage { get { return m_staticTrashMisDamage; } }
    public static  float disposeFailDamage { get { return m_staticDisposeFailDamage; } }


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

        m_staticPlatformGenerator = m_platformGenerator;
        m_staticTrashGenerator = m_trashGenerator;
        m_staticObstacleGenerator = m_obstacleGenerator;
        m_staticPowerupGenerator = m_powerupGenerator;
        m_staticCharacterGenerator = m_characterGenerator;

        m_staticPlatformSpeed = m_platformSpeed;
        m_staticTrashMisDamage = m_trashMissDamage;
        m_staticDisposeFailDamage = m_disposeFailDamage;
    }



}