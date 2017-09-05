using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPowerup : MonoBehaviour {

    public enum Type
    {
        QuiExurga,
        Droceo_Drone,
        Orb_Repair
    }

    [SerializeField]
    protected GameObject m_parentObject;
    [SerializeField]
    private bool m_justDisable;
    protected float m_duration;

    public abstract Type type { get; }

    protected virtual void Death()
    {

    }

    public void UpdateInfo()
    {
        var database = GlobalGameSettings.Instance.powerupDatabase.GetDatabase(type);
        var currentLevel = GameManager.Instance.GetSystem<PlayerProfile>().GetCurrentPowerupLevel(type);
        m_duration = database.GetDuration(currentLevel);
    }

    protected void UpdateDuration(float duration)
    {
        m_duration = duration;
    }

    protected abstract void PowerupFunction();

    protected IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(m_duration);
        Death();
        if (m_justDisable)
        {
            m_parentObject.SetActive(false);
        }
        else
        {
            Destroy(m_parentObject);
        }
    }

    private void OnLevelStartEvent(LevelStartEvent e)
    {
        UpdateInfo();
    }

    private void OnEnable()
    {
        this.AddGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

    private void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }
}
