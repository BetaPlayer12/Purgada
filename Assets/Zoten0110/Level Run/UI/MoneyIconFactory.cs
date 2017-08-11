using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyEvent : GameEvent
{
    public int amount;
    public Vector3 screenPos;

    public MoneyEvent(GameObject sender, int amount, Vector3 screenPos) : base(sender)
    {
        this.amount = amount;
        this.screenPos = screenPos;
    }
}

public class MoneyIconFactory : MonoBehaviour {

    [SerializeField]
    private GameObject m_moneyIcon;
    [SerializeField]
    private Color m_positive;
    [SerializeField]
    private Color m_negative;


    private void OnMoneyEvent(MoneyEvent e)
    {
        var icon = InstantiateMoney(e.amount);
        icon.transform.position = e.screenPos;
    }

    private GameObject InstantiateMoney(int amount)
    {
        var icon = Instantiate(m_moneyIcon, transform) as GameObject;
       
        var textMesh = icon.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh)
        {
            textMesh.text = amount.ToString();
            textMesh.color = amount > 0 ? m_positive : m_negative;
        }

        return icon;
    }

    private void OnEnable()
    {
        this.AddGameEventListenerGlobal<MoneyEvent>(OnMoneyEvent);
    }

    private void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<MoneyEvent>(OnMoneyEvent);
    }
}
