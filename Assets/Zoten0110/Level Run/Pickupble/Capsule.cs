using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : IPickup
{
    [SerializeField]
    private float m_healValue;

    protected override void OnPickup(GameObject GO)
    {
        var playerHealth = GO.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            playerHealth.Heal(m_healValue);
            this.RaiseGameEventGlobal<ShowCommentEvent>(new ShowCommentEvent(gameObject, "You feel better"));
        }
    }
}
