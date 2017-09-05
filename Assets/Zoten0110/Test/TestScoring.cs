using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestScoring : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private float m_scorePerSecond;

    private bool Active;
    private float timer;

    private int score { get { return (int)(timer * m_scorePerSecond); } }

    private void Start()
    {
        timer = 0;
        m_text.text = score.ToString();
    }

    // Update is called once per frame
    void Update () {
        if (Active)
        {
            timer += Time.deltaTime;
        }
	}
}
