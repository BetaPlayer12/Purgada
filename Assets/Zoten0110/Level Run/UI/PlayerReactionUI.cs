using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReactionUI : MonoBehaviour {

    public enum Reaction
    {
        Normal,
        Damaged,
        Healed,
        Nauseated,
        Dizzy,
        Frozen
    }

    [SerializeField]
    private Image m_playerFaceImage;

    [SerializeField]
    private Sprite m_normalFace;
    [SerializeField]
    private Sprite m_damageFace;
    [SerializeField]
    private Sprite m_healFace;
    [SerializeField]
    private Sprite m_nauseaFace;
    [SerializeField]
    private Sprite m_dizzyFace;
    [SerializeField]
    private Sprite m_frozenFace;

    private bool m_isInfected;
    private bool m_otherFaceAnimation;
    private DiseaseType m_diseaseInfected;

    public void OverrideFaceAnimation(Reaction reaction, float time)
    {
        StopAllCoroutines();
        StartCoroutine(FaceAnimation(reaction, time));
    }

    private void OnInflictDiseaseToPlayerEvent(InflictDiseaseToPlayerEvent e)
    {
        m_isInfected = true;
        m_diseaseInfected = e.diseaseType;
        StopAllCoroutines();
        m_playerFaceImage.overrideSprite = null;
    }

    private void OnDiseaseEndEvent(DiseaseEndEvent e) =>
        m_isInfected = false;

    private void OnPlayerDamageEvent(PlayerDamageEvent e) =>
        OverrideFaceAnimation(Reaction.Damaged, 1f);

    private IEnumerator FaceAnimation(Reaction reaction, float time)
    {
        m_otherFaceAnimation = true;
        switch (reaction)
        {
            case Reaction.Normal:
                m_playerFaceImage.overrideSprite = m_normalFace;
                break;
            case Reaction.Damaged:
                m_playerFaceImage.overrideSprite = m_damageFace;
                break;
            case Reaction.Healed:
                m_playerFaceImage.overrideSprite = m_healFace;
                break;
            case Reaction.Nauseated:
                m_playerFaceImage.overrideSprite = m_nauseaFace;
                break;
            case Reaction.Dizzy:
                m_playerFaceImage.overrideSprite = m_dizzyFace;
                break;
            case Reaction.Frozen:
                m_playerFaceImage.overrideSprite = m_frozenFace;
                break;
        }

        yield return new WaitForSeconds(time);

        m_playerFaceImage.overrideSprite = null;
    }

    private void Update()
    {
        if (m_isInfected)
        {
            switch (m_diseaseInfected)
            {
                case DiseaseType.Nausea:
                    m_playerFaceImage.sprite = m_nauseaFace;
                    break;

                case DiseaseType.Repiratory:
                    m_playerFaceImage.sprite = m_dizzyFace;
                    break;
                case DiseaseType.Tetanus:
                    m_playerFaceImage.sprite = m_frozenFace;
                    break;
            }
        }
        else
        {
            m_playerFaceImage.sprite = m_normalFace;
        }
    }

    private void OnEnable()
    {
        this.AddGameEventListenerGlobal<InflictDiseaseToPlayerEvent>(OnInflictDiseaseToPlayerEvent);
        this.AddGameEventListenerGlobal<DiseaseEndEvent>(OnDiseaseEndEvent);
        this.AddGameEventListenerGlobal<PlayerDamageEvent>(OnPlayerDamageEvent);
    }

    private void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<InflictDiseaseToPlayerEvent>(OnInflictDiseaseToPlayerEvent);
        this.RemoveGameEventListenerGlobal<DiseaseEndEvent>(OnDiseaseEndEvent);
        this.RemoveGameEventListenerGlobal<PlayerDamageEvent>(OnPlayerDamageEvent);
    }
}
