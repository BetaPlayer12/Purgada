using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(TrashDatabase))]
public class TrashDatabase_Editor : IDatabase_Editor
{
    private SerializedProperty m_overrideTrashProp = null;
    private SerializedProperty m_overrideTypeProp = null;

    protected override void DisplayAdditionalFoldoutDetails(SerializedProperty entryProp)
    {
        var entryTrashProp = entryProp.FindPropertyRelative("m_trash");
        var entryTypeProp = entryProp.FindPropertyRelative("m_type");

        EditorGUILayout.LabelField("Type: " + ((Trash.Type)entryTypeProp.enumValueIndex).ToString());
        DisplayTexture("Trash: ", entryTrashProp, true);
    }

    protected override void DisplayEditableFoldout()
    {
        EditorGUILayout.PropertyField(m_overrideTypeProp);
        EditorGUILayout.PropertyField(m_overrideTrashProp);
    }

    protected override void OnLoadModule()
    {
        m_overrideTrashProp = m_serializedObject.FindProperty("m_overrideTrash");
        m_overrideTypeProp = m_serializedObject.FindProperty("m_overrideType");
    }

    protected override void OnUnloadModule()
    {
        var systemScript = ((TrashDatabase)m_systemScript);
        systemScript.UpdateSeperateEntries();
        systemScript.UpdateTrashComponents();
    }

    protected override void OverrideEditableValues(SerializedProperty entryProp)
    {
        var entryTrashProp = entryProp.FindPropertyRelative("m_trash");
        var entryTypeProp = entryProp.FindPropertyRelative("m_type");

        m_overrideTrashProp.objectReferenceValue = entryTrashProp.objectReferenceValue;
        m_overrideTypeProp.enumValueIndex = entryTypeProp.enumValueIndex;
    }

    protected override void SaveAdditionalEntryInfo(SerializedProperty editedEntryProp)
    {
        var entryTrashProp = editedEntryProp.FindPropertyRelative("m_trash");
        var entryTypeProp = editedEntryProp.FindPropertyRelative("m_type");

        entryTrashProp.objectReferenceValue = m_overrideTrashProp.objectReferenceValue;
        entryTypeProp.enumValueIndex = m_overrideTypeProp.enumValueIndex;
    }
}
