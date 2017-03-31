using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the main menu
/// Functions involving the entire main menu such as opening of windows are placed here
/// </summary>
public class SceneManagerMainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_hiddenPanel;
    [SerializeField]
    private ShopWindow m_shop;
    [SerializeField]
    private BackgroundSelectWindow m_backgroundSelect;

    public void OnButtonShop()
    {
        var shopGO = m_shop.gameObject;
        shopGO.SetActive(!shopGO.activeInHierarchy);
        ActivateHiddenPanel(shopGO.activeInHierarchy);
    }

    public void OnButtonBackgroundSelect()
    {
        var backgroundSelectGO = m_backgroundSelect.gameObject;
        backgroundSelectGO.SetActive(!backgroundSelectGO.activeInHierarchy);
        ActivateHiddenPanel(backgroundSelectGO.activeInHierarchy);
    }

    private void ActivateHiddenPanel(bool value)
    {
        m_hiddenPanel.SetActive(value);
    }

    void Start()
    {
    }
}
