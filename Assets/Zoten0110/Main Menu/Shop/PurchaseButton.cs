using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour {

    private Button m_button;
    [SerializeField]
    private GameObject m_soldOutImage;

    public void SoldOut()
    {
        m_soldOutImage.SetActive(true);
        MakeNonInteractable();
    }

    public void MakeNonInteractable()
    {
        m_button.interactable = false;
    }

    private void Start()
    {
        m_button = GetComponent<Button>();
    }

}
