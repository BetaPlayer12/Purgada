using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectDropRate))]
public class ObjectDropRate_Editor : Editor
{
    private ObjectDropRate m_system;
    private SerializedProperty m_dropListProp;

    private LoadShareManager m_dropRateManager;
    private bool m_inEditMode;
    private int m_editIndex;

    private void OnEnable()
    {
        m_system = target as ObjectDropRate;
        m_dropListProp = serializedObject.FindProperty("m_dropList");

        Load[] list = new Load[m_dropListProp.arraySize];

        for (int i = 0; i < m_dropListProp.arraySize; i++)
        {
            list[i] = new Load(m_dropListProp.GetArrayElementAtIndex(i).FindPropertyRelative("m_dropRate"));
        }

        m_dropRateManager = new LoadShareManager(list);
    }

    private void OnDisable()
    {

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        m_dropRateManager.Operate();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            m_dropListProp.arraySize++;
            m_dropRateManager.Add(new Load(m_dropListProp.GetArrayElementAtIndex(m_dropListProp.arraySize - 1).FindPropertyRelative("m_dropRate")));
            m_editIndex = m_dropListProp.arraySize - 1;
            m_inEditMode = true;
        }

        if (m_dropListProp.arraySize != 0)
        {
            if (GUILayout.Button("Equalize"))
            {
                m_dropRateManager.MakeEqual();
            }
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (m_inEditMode)
        {
            DisplayEdit();
        }
        else
        {
            if(m_dropListProp.arraySize == 0)
            {
                EditorGUILayout.LabelField("No entries");
            }
            else
            {
                DisplayList();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayList()
    {
        for (int i = 0; i < m_dropListProp.arraySize; i++)
        {
            var property = m_dropListProp.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Edit"))
            {
                m_inEditMode = true;
                m_editIndex = i;
            }
            if (GUILayout.Button("Delete"))
            {
                m_dropRateManager.RemoveAt(i);
                m_system.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();
            m_dropRateManager.PropertyField(i, property.FindPropertyRelative("m_name").stringValue);

            SerializedProperty dropObject = property.FindPropertyRelative("m_object");
            if (dropObject.objectReferenceValue)
            {
                EditorExtentions.DisplayTexture(dropObject.objectReferenceValue.name, dropObject, true);
            }
        }
    }

    private void DisplayEdit()
    {
        var property = m_dropListProp.GetArrayElementAtIndex(m_editIndex);

      
        EditorGUILayout.PropertyField(property.FindPropertyRelative("m_name"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("m_object"));

        if (GUILayout.Button("Save"))
        {
            m_inEditMode = false;
        }
      
    }
}


