using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseButton : MonoBehaviour {

    private Button m_button;
    [SerializeField]
    private Text m_text;
    [SerializeField]
    private TextMeshProUGUI m_textMesh;
    [SerializeField]
    private GameObject m_soldOutImage;

    public void SoldOut()
    {
        m_soldOutImage.SetActive(true);
        MakeNonInteractable();
    }

    public void SetCost(int value)
    {
        if (m_text)
        {
            m_text.text = value.ToString();
        }

        if (m_textMesh)
        {
            m_textMesh.text = value.ToString();
        }
    }

    public void SetCost(float value)
    {
        if (m_text)
        {
            m_text.text = value.ToString();
        }

        if (m_textMesh)
        {
            m_textMesh.text = value.ToString();
        }
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
