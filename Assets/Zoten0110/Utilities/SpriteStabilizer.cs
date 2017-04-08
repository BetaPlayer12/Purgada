using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SpriteStability
{
     Vector2 GetReference();
}

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteStabilizer : MonoBehaviour {
    
    [SerializeField]
    private Vector2 m_screenReference;
    public bool m_isIndependent;


    // Use this for initialization
    void Start () {

        if (!m_isIndependent)
        {
            Debug.LogWarning("Connect to Reference");
        }
        
       
    }

    void Update()
    {
        transform.localScale = new Vector3(Screen.width / m_screenReference.x, Screen.height / m_screenReference.y, 1);
    }
}
