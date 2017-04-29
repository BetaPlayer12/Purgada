using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEvent : GameEvent
{
    public PlayerDeathEvent(GameObject senderObj) : base(senderObj)
    {
        sender = senderObj;
    }
}

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private float m_maxHealth;
    [SerializeField]///Debug;
    private float m_currentHealth;

    private bool m_invulnerable;
    private bool m_dead;

    public void BecomeInvulnerable(bool value) =>
        m_invulnerable = value;

    public void Damage(float damage)
    {
        if (m_invulnerable)
            return;

        m_currentHealth -= damage;

        if (m_currentHealth <= 0f)
        {
            this.RaiseEventGlobal(new PlayerDeathEvent(gameObject));
            m_dead = true;
        }
    }

    public void Heal(float heal)
    {
        m_currentHealth += heal;

        if(m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
    }

    private void OnTrashMissedEvent(TrashMissedEvent e) =>
        Damage(GameManager.Instance.globalGameSettings.trashMissDamage);

    // Use this for initialization
    void Start ()
    {
        m_currentHealth = m_maxHealth;
        m_dead = false;
    }

    void OnEnable()
    {
        this.AddEventListenerGlobal<TrashMissedEvent>(OnTrashMissedEvent);
    }

    void OnDisable()
    {
        this.RemoveEventListenerGlobal<TrashMissedEvent>(OnTrashMissedEvent);
    }
}
