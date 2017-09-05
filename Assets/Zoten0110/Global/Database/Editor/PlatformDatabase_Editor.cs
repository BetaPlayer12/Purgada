using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlatformDatabase))]
public class PlatformDatabase_Editor : IDatabase_Editor
{
    private SerializedProperty m_overridePlatformProp = null;

    protected override void DisplayAdditionalFoldoutDetails(SerializedProperty entryProp)
    {
        var entryPlatformProp = entryProp.FindPropertyRelative("m_platform");

        DisplayTexture("Platform: ", entryPlatformProp, true);
    }

    protected override void DisplayEditableFoldout()
    {
        EditorGUILayout.PropertyField(m_overridePlatformProp);
    }

    protected override void OnLoadModule()
    {
        m_overridePlatformProp = m_serializedObject.FindProperty("m_overridePlatform");
    }

    protected override void OnUnloadModule()
    {

    }

    protected override void OverrideEditableValues(SerializedProperty entryProp)
    {
        var entryPlatformProp = entryProp.FindPropertyRelative("m_platform");

        m_overridePlatformProp.objectReferenceValue = entryPlatformProp.objectReferenceValue;
    }

    protected override void SaveAdditionalEntryInfo(SerializedProperty editedEntryProp)
    {
        var entryPlatformProp = editedEntryProp.FindPropertyRelative("m_platform");

        entryPlatformProp.objectReferenceValue = m_overridePlatformProp.objectReferenceValue;
    }
}
