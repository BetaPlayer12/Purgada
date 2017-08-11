using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveProfileEvent : SystemEvent { }
public class LoadProfileEvent : SystemEvent { }

public class SaveLoadManager : ISystem {

    [SerializeField]
    private PlayerProfile m_playerProfile;
    [SerializeField]
    private bool m_saveOnQuit;

    [Header("Setting")]
    [SerializeField]
    private bool m_allowLoad = true;
    [SerializeField]
    private bool m_allowSave = true;

    private const string m_currentMoney = "playerMoney";
    private const string m_ownedToken = "ownedToken";
    private const string m_quiExurga = "quiExurgaLevel";
    private const string m_droceoDrone = "droceoDroneLevel";
    private const string m_orbRepair = "orbRepairLevel";

    private void Save()
    {
        if (m_allowSave)
        {
            var save = m_playerProfile.Save();
            PlayerPrefs.SetInt(m_currentMoney, save.currentMoney);
            for (int i = 0; i < save.ownedTokens.Length; i++)
            {
                PlayerPrefs.SetInt(m_ownedToken + i.ToString(), save.ownedTokens[i]);
            }

            PlayerPrefs.SetInt(m_quiExurga, save.quiExurgaLevel);
            PlayerPrefs.SetInt(m_droceoDrone, save.droceoDroneLevel);
            PlayerPrefs.SetInt(m_orbRepair, save.orbRepairLevel);

            Debug.Log("Saved");
        }
    }

    private void Load()
    {
        if (m_allowLoad)
        {
            PlayerInfo playerInfo = new PlayerInfo();

            playerInfo.currentMoney = PlayerPrefs.GetInt(m_currentMoney, m_playerProfile.playerMoney.startingMoney);
            playerInfo.ownedTokens = new int[(int)TokenTypes._Count];

            //Enum to int
            {
                for (int i = 0; i < playerInfo.ownedTokens.Length; i++)
                {
                    playerInfo.ownedTokens[i] = PlayerPrefs.GetInt(m_ownedToken + i.ToString(), 0);
                }
            }

            playerInfo.quiExurgaLevel = PlayerPrefs.GetInt(m_quiExurga, 0);
            playerInfo.droceoDroneLevel = PlayerPrefs.GetInt(m_droceoDrone, 0);
            playerInfo.orbRepairLevel = PlayerPrefs.GetInt(m_orbRepair, 0);

            m_playerProfile.Load(playerInfo);
            Debug.Log("Loaded");
        }
    }

    private void OnApplicationQuit()
    {
        if (m_saveOnQuit)
        {
            Save();
        }
    }

    private void OnSaveProfileEvent(SaveProfileEvent e)
    {
        Save();
    }

    private void OnLoadProfileEvent(LoadProfileEvent e)
    {
        Load();
    }

    private void OnEnable()
    {
        this.AddSystemEventListenerGlobal<SaveProfileEvent>(OnSaveProfileEvent);
        this.AddSystemEventListenerGlobal<LoadProfileEvent>(OnLoadProfileEvent);
    }

    private void OnDisable()
    {
        this.RemoveSystemEventListenerGlobal<SaveProfileEvent>(OnSaveProfileEvent);
        this.RemoveSystemEventListenerGlobal<LoadProfileEvent>(OnLoadProfileEvent);
    }
}
