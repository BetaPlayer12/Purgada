using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilCanon_Projectile : MonoBehaviour {

    private void OnSuccesfulDisposal()
    {
        Debug.Log("Soil Canon Disposed Trash");
    }

    private void OnFailedDisposal()
    {
        Debug.Log("Soil Canon Fails");
    }

    private void OnHit(Trash trash)
    {
        if(trash != null)
        {
            if(trash.trashType == Trash.Type.Organic)
            {
                OnSuccesfulDisposal();
            }
            else
            {
                OnFailedDisposal();
            }
            Destroy(trash.gameObject);
        }
        
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("SoilCanon Hit");
        OnHit(other.gameObject.GetComponentInParent<Trash>());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("SoilCanon Hit");
        OnHit(other.gameObject.GetComponentInParent<Trash>());
    }
}
