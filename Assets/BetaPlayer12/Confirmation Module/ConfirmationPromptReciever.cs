using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationEvent : SystemEvent
{
    public string header;
    public string message;
    public bool isPurchase;
    public ConfirmationWindow.ConfirmationResponseFunc confirmationResponse = null;

    public ConfirmationEvent(string header, string message, bool isPurchase, ConfirmationWindow.ConfirmationResponseFunc confirmationResponse = null)
    {
        this.header = header;
        this.message = message;
        this.isPurchase = isPurchase;
        this.confirmationResponse = confirmationResponse;
    }

}

public class ConfirmationPromptReciever : MonoBehaviour
{
    [SerializeField]
    private ConfirmationWindow m_window;

    private void OnConfirmationEvent(ConfirmationEvent e)
    {
        m_window.ShowWindow(e.header, e.message, e.isPurchase, e.confirmationResponse);
    }


    private void OnEnable()
    {
        this.AddSystemEventListenerGlobal<ConfirmationEvent>(OnConfirmationEvent);
    }

    public void OnDisable()
    {
        this.RemoveSystemEventListenerGlobal<ConfirmationEvent>(OnConfirmationEvent);
    }

}
