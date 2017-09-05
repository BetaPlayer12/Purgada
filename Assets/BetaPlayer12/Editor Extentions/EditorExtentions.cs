using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System;
public static class EditorExtentions{

    /// <summary>
    /// Creates A Toggle that interacts with other label
    /// </summary>
    /// <param name="label">Label of the Toggle</param>
    /// <param name="trueProp">Property that changes to true</param>
    /// <param name="falseProp">Properties that changes to false</param>
    public static void CreateMultipleToggle(string label, SerializedProperty trueProp, params SerializedProperty[] falseProp)
    {
        var newToggle = EditorGUILayout.Toggle(label, trueProp.boolValue);

        if (newToggle != trueProp.boolValue)
        {
            trueProp.boolValue = true;
            for (int i = 0; i < falseProp.Length; i++)
            {
                falseProp[i].boolValue = false;
            }
        }
    }


    /// <summary>
    /// Displays the Image of the GameObject
    /// </summary>
    /// <param name="label"></param>
    /// <param name="entryProp"></param>
    /// <param name="usePrefabImage"></param>
    public static void DisplayTexture(string label, SerializedProperty entryProp, bool usePrefabImage = false)
    {
        if (entryProp.objectReferenceValue)
        {
            Texture2D texturePreview = usePrefabImage ? AssetPreview.GetAssetPreview(entryProp.objectReferenceValue) : ((Sprite)entryProp.objectReferenceValue).texture;
            if (texturePreview == null)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(label + ": " + entryProp.objectReferenceValue.name);
                EditorGUILayout.EndHorizontal();
                return;
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label + ": ");
            GUILayout.Label(new GUIContent(texturePreview), GUILayout.Width(100), GUILayout.Height(100));
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.LabelField(label + ": ", "[No Image]");

        }
    }

    /// <summary>
    /// Play the audio clip in editor
    /// </summary>
    /// <param name="clip"></param>
    public static void PlayClip(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "PlayClip",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new System.Type[] {
                typeof(AudioClip)
            },
            null
        );
        method.Invoke(
            null,
            new object[] {
                clip
            }
        );
    }

    /// <summary>
    /// Stop all audio being played in editor
    /// </summary>
    public static void StopAllClips()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass =
              unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "StopAllClips",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new System.Type[] { },
            null
        );
        method.Invoke(
            null,
            new object[] { }
        );
    }
}
