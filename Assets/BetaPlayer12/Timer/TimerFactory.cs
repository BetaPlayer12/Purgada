using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerFactory : Singleton<TimerFactory> {

    [SerializeField]
    private SpriteDatabase m_iconDatabase;
    [SerializeField]
    private GameObject m_timerPrefab;

    [Header("Settings")]
    [SerializeField]
    private float m_Xspacing;
    [SerializeField]
    private float m_Yspacing;
    [SerializeField]
    private int m_maxColumn;

    private List<RectTransform> m_timerList = new List<RectTransform>();

    public void Create(string timerName, float duration)
    {
        var timer = Instantiate(m_timerPrefab) as GameObject;
        timer.transform.parent = transform;
        timer.transform.localScale = Vector3.one;
        var icon = m_iconDatabase.GetEntry(timerName).sprite;

        timer.GetComponent<Image>().sprite = icon;
        timer.GetComponent<Timer>().SetTimer(duration);
        m_timerList.Add(timer.GetComponent<RectTransform>());      
    }

    void Update()
    {
        if(m_timerList.Count == 0)
        {
            return;
        }

        // Remove All NUll
        for (var i = m_timerList.Count - 1; i > -1; i--)
        {
            if (m_timerList[i] == null)
                m_timerList.RemoveAt(i);
        }

        int YSpacingFactor = -1;

        float m_currentX = 0f;
        float m_currentY = 0f;

        for (int i = 0; i < m_timerList.Count; i++)
        {
           if(i % m_maxColumn == 0)
            {
                YSpacingFactor++;
                m_currentX = 0f;
                m_currentY = YSpacingFactor * m_Yspacing;
            }

            m_timerList[i].localPosition = new  Vector3(m_currentX, m_currentY, 0);
            m_currentX += m_Xspacing;
        }
    }

}
