using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IConfirmationHandler : MonoBehaviour {

    protected ConfirmationWindow.ConfirmationResponseFunc confirmationResponse;

    protected abstract string header { get; }
    protected abstract string message { get; }
    protected abstract bool isPurchase { get; }
    protected abstract bool exclusiveResponse { get; }
    

    public void RaiseConfirmation()
    {
        this.RaiseSystemEventGlobal(new ConfirmationEvent(header, message, isPurchase, confirmationResponse));
    }

    public void ConfirmationResponse(ConfirmationWindowMessage.Response response)
    {
        switch (response)
        {
            case ConfirmationWindowMessage.Response.Confirm:
                OnAffirmResponse();
                break;
            case ConfirmationWindowMessage.Response.Cancel:
                OnDeclineResponse();
                break;
        }
    }

    protected abstract void OnAffirmResponse();
    protected abstract void OnDeclineResponse();


    private void Start()
    {
        if (exclusiveResponse)
        {
            confirmationResponse = ConfirmationResponse;
        }
    }
}
