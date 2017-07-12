using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPurchase : IConfirmationHandler
{
    [SerializeField]
    private IPowerup.Type m_powerupType;
    [SerializeField]
    private int m_cost;
    private PurchaseButton m_purhcaseButton;
    private PlayerMoney m_playerMoney;
    private bool m_isMaxed;

    protected override string header
    {
        get
        {
            return "Upgrades";
        }
    }

    protected override string message => $"Would you like to Upgrade " + PowerupTypeToString();

    protected override bool isPurchase => true;

    protected override bool exclusiveResponse => true;

    private string PowerupTypeToString()
    {
        switch (m_powerupType)
        {
            case IPowerup.Type.Droceo_Drone:
                return "Droceo Dron";
            case IPowerup.Type.Orb_Repair:
                return "Orb Repair";
            case  IPowerup.Type.QuiExurga:
                return "QuiExurga";
        }

        return "";
    }

    private void UpdateButton(PowerupLevel level)
    {
        var powerupInfo = GlobalGameSettings.Instance.powerupDatabase.GetDatabase(m_powerupType);
        m_cost = powerupInfo.GetCost(level);
        m_purhcaseButton.SetCost(m_cost);
    }

    protected override void OnAffirmResponse()
    {
        var playerProfile = GameManager.Instance.GetSystem<PlayerProfile>();
        playerProfile.UpgradePowerup(m_powerupType);
        playerProfile.GetComponent<PlayerMoney>().DeductMoney(m_cost);
        m_isMaxed = playerProfile.IsPowerupMaxed(m_powerupType);
        if (!m_isMaxed)
        {
            UpdateButton((PowerupLevel)((int)playerProfile.GetCurrentPowerupLevel(m_powerupType) + 1));
        }
    }

    protected override void OnDeclineResponse()
    {
        throw new NotImplementedException();
    }

    protected override void OnStartModule()
    {
        m_purhcaseButton = GetComponent<PurchaseButton>();
        m_purhcaseButton.SetCost(m_cost);
    }

    private void OnEnable()
    {
        var playerProfile = GameManager.Instance.GetSystem<PlayerProfile>();
        m_playerMoney = playerProfile.GetComponent<PlayerMoney>();
        m_isMaxed = playerProfile.IsPowerupMaxed(m_powerupType);
        if (!m_isMaxed)
        {
            UpdateButton((PowerupLevel)((int)playerProfile.GetCurrentPowerupLevel(m_powerupType) + 1));
        }
    }

    private void Update()
    {
        if (!m_isMaxed)
        {
            if (m_playerMoney.currentMoney < m_cost)
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
