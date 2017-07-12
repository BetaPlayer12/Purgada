using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TokenPurchase : IConfirmationHandler
{

    [SerializeField]
    private TokenTypes m_tokenType;
    [SerializeField]
    private int m_cost;
    private PurchaseButton m_purhcaseButton;
    private PlayerMoney m_playerMoney;
    private bool m_isPurchased;

    protected override string header
    {
        get
        {
            return "Purchases";
        }
    }

    protected override string message => $"Would you like to perchase " + TokenTypeToString();

    protected override bool isPurchase => true;

    protected override bool exclusiveResponse => true;


    private string TokenTypeToString()
    {
        switch (m_tokenType)
        {
            case TokenTypes.Booster_Shot:
                return "Booster Shot";
            case TokenTypes.Miracle_Drug:
                return "Miracle Drug";
            case TokenTypes.Sponsorship:
                return "Sponsorship";
        }

        return "";
    }


    protected override void OnAffirmResponse()
    {
        var playerProfile = GameManager.Instance.GetSystem<PlayerProfile>();
        playerProfile.AddToken(m_tokenType);
        playerProfile.GetComponent<PlayerMoney>().DeductMoney(m_cost);
        m_purhcaseButton.SoldOut();
    }

    protected override void OnDeclineResponse()
    {
        Debug.Log("Cancelled");
    }

    protected override void OnStartModule()
    {
        m_purhcaseButton = GetComponent<PurchaseButton>();
        m_purhcaseButton.SetCost(m_cost);
    }

    private void OnEnable()
    {
        var playerProfile = GameManager.Instance.GetSystem<PlayerProfile>();
        m_isPurchased = playerProfile.isTokenOwned(m_tokenType);
        m_playerMoney = playerProfile.GetComponent<PlayerMoney>();
    }

    private void Update()
    {
        if (!m_isPurchased)
        {
            if(m_playerMoney.currentMoney < m_cost)
            {
                m_purhcaseButton.MakeNonInteractable();
            }
        }
        else
        {
            m_purhcaseButton.SoldOut();
        }
       
    }
}
