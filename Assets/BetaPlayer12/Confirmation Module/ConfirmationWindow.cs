using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmationWindowMessage : SystemEvent
{
    public enum Response
    {
        Confirm,
        Cancel,
        Yes = Confirm,
        No = Cancel
    }

    public Response m_response;
}

public class ConfirmationWindow : MonoBehaviour {

    public delegate void ConfirmationResponseFunc(ConfirmationWindowMessage.Response response);

    public Text m_confirmHeader;
    public Text m_confirmMessage;

    [SerializeField]
    private Text m_buttonConfirmText;
    [SerializeField]
    private Text m_buttonCancelText;

    private ConfirmationResponseFunc ConfirmationResponse;

    public void ShowWindow(string header, string message, bool isPurchase, ConfirmationResponseFunc action = null)
    {
        gameObject.SetActive(true);
        m_confirmHeader.text = header;
        m_confirmMessage.text = message;

        m_buttonConfirmText.text = isPurchase ? "Buy" : "Yes";
        m_buttonCancelText.text = isPurchase ? "Cancel" : "No";

        ConfirmationResponse = action;
    }

    public void ConfirmAction()
    {
        if (ConfirmationResponse == null)
        {
            this.RaiseSystemEventGlobal<ConfirmationWindowMessage>(new ConfirmationWindowMessage() { m_response = ConfirmationWindowMessage.Response.Confirm });
        }
        else
        {
            ConfirmationResponse(ConfirmationWindowMessage.Response.Confirm);
            ConfirmationResponse = null;
        }
        gameObject.SetActive(false);

    }

    public void CancelAction()
    {
        if (ConfirmationResponse == null)
        {
            this.RaiseSystemEventGlobal<ConfirmationWindowMessage>(new ConfirmationWindowMessage() { m_response = ConfirmationWindowMessage.Response.Cancel });
        }
        else
        {
            ConfirmationResponse(ConfirmationWindowMessage.Response.Cancel);
            ConfirmationResponse = null;
        }

        gameObject.SetActive(false);
    }
}
