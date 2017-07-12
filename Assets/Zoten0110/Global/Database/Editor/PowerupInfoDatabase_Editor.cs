using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PowerupInfoDatabase))]
public class PowerupInfoDatabase_Editor : Editor {

    private SerializedProperty m_infoListProp;

    void OnEnable()
    {
        m_infoListProp = serializedObject.FindProperty("m_infoList");
        var systemScript = target as PowerupInfoDatabase;
        systemScript.UpdateList();
    }

    void OnDisable()
    {

    }

    public override void OnInspectorGUI()
    {
        //Updating Original Script
        {
            serializedObject.Update();
            EditorGUIExt.ScriptPropertyField(serializedObject);
            serializedObject.ApplyModifiedProperties();
        }

        serializedObject.Update();
        EditorGUIExt.ScriptAndAssetPropertyFields(serializedObject);

        EditorGUILayout.Space();

        for (int i = 0; i < m_infoListProp.arraySize; i++)
        {
            DisplayInfo(m_infoListProp.GetArrayElementAtIndex(i), i + 1);
        }

        serializedObject.ApplyModifiedProperties();
    }


    private void DisplayInfo(SerializedProperty infoProp, int levelIndex)
    {
        var costProp = infoProp.FindPropertyRelative("m_cost");
        var durationProp = infoProp.FindPropertyRelative("m_duration");

        EditorGUILayout.LabelField("Level " + levelIndex.ToString());
        durationProp.floatValue = EditorGUILayout.FloatField("Duration", durationProp.floatValue);
        costProp.intValue = EditorGUILayout.IntField("Cost", costProp.intValue);
        EditorGUILayout.Space();
    }
}
