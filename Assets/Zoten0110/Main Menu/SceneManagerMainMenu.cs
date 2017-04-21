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


    private Animator m_animator;

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

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void ToLevelRun()
    {
        m_animator.SetBool("Level Run", true);
    }

    private void ActivateHiddenPanel(bool value)
    {
        m_hiddenPanel.SetActive(value);
    }


    void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    void Start()
    {
        Time.timeScale = 1;
    }
}
