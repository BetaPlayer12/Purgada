using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSummary : MonoBehaviour {

    [SerializeField]
    private GameObject m_window;

    private void OnPlayerDeathEvent (PlayerDeathEvent e)
    {
        m_window.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetLeve()
    {
        SceneManager.LoadScene("Game");
    }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<PlayerDeathEvent>(OnPlayerDeathEvent);
        GameManager.Instance.GetSystem<PlayerProfile>().ResetTokens();
    }

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<PlayerDeathEvent>(OnPlayerDeathEvent);
    }

}
