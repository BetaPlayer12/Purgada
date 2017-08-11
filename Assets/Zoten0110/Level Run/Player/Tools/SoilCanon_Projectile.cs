using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilCanon_Projectile : MonoBehaviour {

    [SerializeField]
    private GameObject m_successfulDirt;
    [SerializeField]
    private GameObject m_failDirt;
    [SerializeField]
    private GameObject m_splashFX;

    private void OnSuccesfulDisposal(Trash trash)
    {
        Debug.Log("Soil Canon Disposed Trash");

        var dirtPile =Instantiate(m_successfulDirt, transform.position, Quaternion.identity);
        dirtPile.transform.parent = trash.transform.parent;
    }

    private void OnFailedDisposal(Trash trash)
    {
        Debug.Log("Soil Canon Fails");

        var dirtPile = Instantiate(m_failDirt, transform.position, Quaternion.identity);
        dirtPile.transform.parent = trash.transform.parent;
    }

    private void OnHit(Trash trash,Transform parent)
    {
        if(trash != null)
        {
            if(trash.trashType == Trash.Type.Organic)
            {
                OnSuccesfulDisposal(trash);
            }
            else
            {
                OnFailedDisposal(trash);
            }
            Destroy(trash.gameObject);
        }
       var splatter =  Instantiate(m_splashFX) as GameObject;
        splatter.transform.position = transform.position;
        splatter.transform.parent = parent;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("SoilCanon Hit");
        OnHit(other.gameObject.GetComponentInParent<Trash>(), other.gameObject.transform.parent);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("SoilCanon Hit " + other.gameObject.name);
        //OnHit(other.gameObject.GetComponentInParent<Trash>());
    }
}
