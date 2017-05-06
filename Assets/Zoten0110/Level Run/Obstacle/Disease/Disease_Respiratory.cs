using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Disease_Respiratory : IDisease
{
    [SerializeField]
    private BlurOptimized m_blur;

    [SerializeField]
    private float m_maxBlur;
    [SerializeField]
    private float m_minBlur;
    [SerializeField]
    private float m_speed;

    private IEnumerator BlurEffect()
    {
        while (m_blur.blurSize < m_maxBlur)
        {
            m_blur.blurSize += m_speed * Time.deltaTime;
            yield return null;
        }

        bool increaseBlur = false;
        while (true)
        {
            if (increaseBlur)
            {
                m_blur.blurSize += m_speed * Time.deltaTime;

                if (m_blur.blurSize > m_maxBlur)
                {
                    increaseBlur = false;
                }
            }
            else
            {

                m_blur.blurSize -= m_speed * Time.deltaTime;

                if (m_blur.blurSize < m_minBlur)
                {
                    increaseBlur = true;
                }
            }

            yield return null;
        }
    }

    private IEnumerator BlurEffectEnd()
    {
        while (m_blur.blurSize > 0)
        {
            m_blur.blurSize -= m_speed * Time.deltaTime;
            yield return null;
        }
    }

    protected override void DiseaseEnd()
    {
        StopAllCoroutines();
        StartCoroutine(BlurEffectEnd());
    }

    protected override void DiseaseStart()
    {
        StartCoroutine(BlurEffect());
    }


}
