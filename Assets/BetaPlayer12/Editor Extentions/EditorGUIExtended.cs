using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorGUIExtension {

    public delegate void GUIFunction();

    #region MultipleToggle
    public struct Toggle
    {
        public string label;
        public SerializedProperty property;
    }

    /// <summary>
    /// Creates A Toggle that interacts with other label
    /// </summary>
    /// <param name="label">Label of the Toggle</param>
    /// <param name="trueToggle">Property that changes to true</param>
    /// <param name="falseProp">Properties that changes to false</param>
    private static void MultipleToggleInteraction(Toggle trueToggle, params Toggle[] toggleListProp)
    {
        var newToggle = EditorGUILayout.Toggle(trueToggle.label, trueToggle.property.boolValue,EditorStyles.radioButton);

        if (newToggle != trueToggle.property.boolValue)
        {
            trueToggle.property.boolValue = true;
            for (int i = 0; i < toggleListProp.Length; i++)
            {
                if (toggleListProp[i].property != trueToggle.property)
                {
                    toggleListProp[i].property.boolValue = false;
                }
            }
        }
    }



    public static void MultipleToggle(string label,params Toggle[] toggleListProp)
    {
        for (int i = 0; i < toggleListProp.Length; i++)
        {
            var toggle = toggleListProp[i];
            MultipleToggleInteraction(toggle,toggleListProp);
        }
    }
    #endregion

    /// <summary>
    /// A Foldout that automatically has its expanded form dictated in a function
    /// </summary>
    /// <param name="label">Label of the foldout</param>
    /// <param name="value"></param>
    /// <param name="Content">Content of the Expanded Foldout</param>
    /// <returns></returns>
    public static bool Foldout(string label,bool value,GUIFunction Content)
    {
        var newValue = EditorGUILayout.Foldout(value, label);
        if (newValue)
        {
            EditorGUI.indentLevel++;
                Content();
            EditorGUI.indentLevel--;
        }
        return newValue;
    }

    /// <summary>
    /// When the value is true, it expands
    /// </summary>
    /// <param name="label"></param>
    /// <param name="value"></param>
    /// <param name="Content"></param>
    /// <returns></returns>
    public static bool Enabler(string label, bool value, GUIFunction Content)
    {
        var newValue = EditorGUILayout.Toggle(value, label);
        if (newValue)
        {
            EditorGUI.indentLevel++;
            Content();
            EditorGUI.indentLevel--;
        }
        return newValue;
    }
}
