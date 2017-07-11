using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenHandler : MonoBehaviour {

    [SerializeField]
    private IToken[] m_tokens;

    protected void OnLevelStart(LevelStartEvent e)
    {
        var playerProfile = GameManager.Instance.GetSystem<PlayerProfile>();

        for (int i = 0; i < m_tokens.Length; i++)
        {
            var token = m_tokens[i];
            if (playerProfile.isTokenOwned(token.type))
            {
                token.MakeActive();
            }
        }
    }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<LevelStartEvent>(OnLevelStart);
    }

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<LevelStartEvent>(OnLevelStart);
    }
}
