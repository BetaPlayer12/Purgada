using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleDatabase))]
public class ObstacleDatabase_Editor : IDatabase_Editor
{
    private SerializedProperty m_overrideObstacleProp = null;
    private SerializedProperty m_overrideTypeProp = null;

    protected override void DisplayAdditionalFoldoutDetails(SerializedProperty entryProp)
    {
        var entryObstacleProp = entryProp.FindPropertyRelative("m_obstacle");
        var entryEntityInstantiatedProp = entryProp.FindPropertyRelative("m_entityInstantiated");

        if (entryEntityInstantiatedProp.boolValue)
        {
            EditorGUILayout.LabelField("Entity Restricted");
        }
        DisplayTexture("Obstacle: ", entryObstacleProp, true);
    }

    protected override void DisplayEditableFoldout()
    {
        EditorGUILayout.PropertyField(m_overrideTypeProp);
        EditorGUILayout.PropertyField(m_overrideObstacleProp);
    }

    protected override void OnLoadModule()
    {
        m_overrideObstacleProp = m_serializedObject.FindProperty("m_overrideObstacle");
        m_overrideTypeProp = m_serializedObject.FindProperty("m_overrideEntityInstantiated");
    }

    protected override void OnUnloadModule()
    {
        var systemScript = ((ObstacleDatabase)m_systemScript);
        systemScript.UpdateSeperateEntries();
    }

    protected override void OverrideEditableValues(SerializedProperty entryProp)
    {
        var entryObstacleProp = entryProp.FindPropertyRelative("m_obstacle");
        var entryEntityInstantiatedProp = entryProp.FindPropertyRelative("m_entityInstantiated");

        m_overrideObstacleProp.objectReferenceValue = entryObstacleProp.objectReferenceValue;
        m_overrideTypeProp.boolValue = entryEntityInstantiatedProp.boolValue;
    }

    protected override void SaveAdditionalEntryInfo(SerializedProperty editedEntryProp)
    {
        var entryObstacleProp = editedEntryProp.FindPropertyRelative("m_obstacle");
        var entryEntityInstantiatedProp = editedEntryProp.FindPropertyRelative("m_entityInstantiated");

        entryObstacleProp.objectReferenceValue = m_overrideObstacleProp.objectReferenceValue;
        entryEntityInstantiatedProp.boolValue = m_overrideTypeProp.boolValue;
    }
}
