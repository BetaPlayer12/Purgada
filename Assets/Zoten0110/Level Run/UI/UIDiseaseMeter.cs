using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDiseaseMeter : MonoBehaviour
{

    [SerializeField]
    private Sprite[] m_profileSprites;
    [SerializeField]
    private Sprite[] m_bacteriaSprites;

    [SerializeField]
    private Image m_bacteria;
    [SerializeField]
    private Image m_profile;
    [SerializeField]
    private Image m_meter;
    [SerializeField]
    private PlayerHealth m_health;

    private bool m_playerIsInfected;

    // Update is called once per frame
    void Update()
    {
        var fillAmount = m_meter.fillAmount = 1f - m_health.currentHealthRatio;

        if (fillAmount <= 0.25)
        {
            m_profile.sprite = m_profileSprites[0];
        }
        else if (fillAmount <= 0.5)
        {
            m_profile.sprite = m_profileSprites[1];
        }
        else
        {
            m_profile.sprite = m_profileSprites[2];
        }

        if (m_playerIsInfected)
        {
            m_bacteria.color = Color.white;
            m_bacteria.sprite = m_bacteriaSprites[5];
        }
        else
        {

            if (fillAmount <= 0.16)
            {
                m_bacteria.color = new Color(1, 1, 1, 0);
            }
            else
            {
                m_bacteria.color = Color.white;
                if (fillAmount <= 0.32)
                {
                    m_bacteria.sprite = m_bacteriaSprites[1];
                }
                else if (fillAmount <= 0.48)
                {
                    m_bacteria.sprite = m_bacteriaSprites[2];
                }
                else if (fillAmount <= 0.64)
                {
                    m_bacteria.sprite = m_bacteriaSprites[3];
                }
                else if (fillAmount <= 0.80)
                {
                    m_bacteria.sprite = m_bacteriaSprites[4];
                }
                else
                {
                    m_bacteria.sprite = m_bacteriaSprites[5];
                }
            }
        }
    }
}
