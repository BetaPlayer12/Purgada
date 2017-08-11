using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommentHandler : MonoBehaviour {

    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private float m_duration;

    private IEnumerator ShowComment(string message)
    {
        m_animator.SetBool("Show", true);
        m_text.text = message;
        yield return new WaitForSeconds(m_duration);
        m_animator.SetBool("Show", false);
    }

    private void Comment(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowComment(message));
    }

    private void OnInflictDiseaseToPlayerEvent (InflictDiseaseToPlayerEvent e)
    {
        switch (e.diseaseType)
        {
            case DiseaseType.Nausea:
                Comment("You feel Nauseous");
                break;
            case DiseaseType.Repiratory:
                Comment("You have a hard time breathing");
                break;
            case DiseaseType.Tetanus:
                Comment("You feel instense pain that running is the only thing you can do is run...don't ask how");
                break;
        }
    }

    private void OnEnable()
    {
        this.AddGameEventListenerGlobal<InflictDiseaseToPlayerEvent>(OnInflictDiseaseToPlayerEvent);
    }

    private void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<InflictDiseaseToPlayerEvent>(OnInflictDiseaseToPlayerEvent);
    }
}
