using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterMovement : MonoBehaviour {

    public enum Direction
    {
        Left = -1,
        Right =1
    }

    public float m_movementSpeed;
    public Transform[] m_spriteTransform;
    private Direction m_direction = Direction.Right;

    public Direction direction { set { m_direction = value; } }

    void Update()
    {
        transform.position += new Vector3(m_movementSpeed * (int)m_direction *Time.deltaTime, 0, 0);

        for (int i = 0; i < m_spriteTransform.Length; i++)
        {
            m_spriteTransform[i].localScale = new Vector3((int)m_direction, 1, 1);
        }
    }
}
