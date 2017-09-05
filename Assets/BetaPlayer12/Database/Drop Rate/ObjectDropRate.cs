using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

///PENDING

/// <summary>
/// Contains a set of objects that can be dropped
/// by something
/// </summary>
public class ObjectDropRate : ScriptableObject
{

    [System.Serializable]
    public struct ObjectInfo
    {
        [SerializeField]
        private string m_name;
        [SerializeField]
        [Range(0, 100)]
        private float m_dropRate;
        [SerializeField]
        private GameObject m_object;

        public ObjectInfo(string name)
        {
            m_name = name;
            m_dropRate = 0f;
            m_object = null;
        }

        public float dropRate { get { return m_dropRate; } }
        public GameObject dropObject { get { return m_object; } }
    }

    [SerializeField]
    private ObjectInfo[] m_dropList;

#if UNITY_EDITOR
    /// <summary>
    /// Remove entry in the index
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAt(int index)
    {
        List<ObjectInfo> list = new List<ObjectInfo>(m_dropList);
        list.RemoveAt(index);
        m_dropList = list.ToArray();
    }
#endif

    /// <summary>
    /// Cant explain right now, it would be better if theres a spectrum
    /// </summary>
    /// <returns></returns>
    public GameObject GetInstanceObject()
    {
        float chance = Random.Range(0f, 100f);
        float accumlatedChance = 0f;
        for (int i = 0; i < m_dropList.Length; i++)
        {
            accumlatedChance += m_dropList[i].dropRate;
            if (accumlatedChance >= chance)
            {
                return m_dropList[i].dropObject;
            }
        }


        return null;
    }
}

// IngredientDrawer
[CustomPropertyDrawer(typeof(ObjectDropRate.ObjectInfo))]
public class IngredientDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x, position.y, 30, position.height);
        var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
        var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
       // EditorGUILayout.LabelField("Name: " + property.FindPropertyRelative("m_name").stringValue);
        EditorGUILayout.PropertyField(property.FindPropertyRelative("m_dropRate"), GUIContent.none);

        SerializedProperty dropObject = property.FindPropertyRelative("m_object");
        if (dropObject.objectReferenceValue)
        {
            EditorExtentions.DisplayTexture(dropObject.objectReferenceValue.name, dropObject, true);
        }

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
