using UnityEditor;
using UnityEngine;

public class EditorGUIExt
{

    private static Color[] m_categoryTypeColors = new Color[] {

        new Color(1.0f, 0.0f, 0.8118f, 0.85f),
        new Color(0.3451f, 0.1098f, 1.0f, 0.85f),
        new Color(1.0f, 0.1725f, 0.0f, 0.85f),
        new Color(0.7765f, 1.0f, 0.0f, 0.85f),
        new Color(1.0f, 0.5216f, 0.0f, 0.85f),
        new Color(1.0f, 0.4392f, 0.5608f, 0.85f),
        new Color(1.0f, 0.8078f, 0.0f, 0.85f),
        new Color(0.0392f, 0.7333f, 1.0f, 0.85f),
        new Color(1.0f, 0.0f, 0.0941f, 0.85f),
        new Color(0.1412f, 0.1843f, 1.0f, 0.85f),
        new Color(1.0f, 0.8745f, 0.0f, 0.85f),
        new Color(0.1725f, 1.0f, 0.0f, 0.85f),
        new Color(0.3451f, 0.1098f, 1.0f, 0.85f),
    };

    public static void ScriptAndAssetPropertyFields(SerializedObject serializedObject)
    {
        ScriptPropertyField(serializedObject);
        AssetPropertyField(serializedObject);
    }

    public static void ScriptPropertyField(SerializedObject serializedObject)
    {
        var UIstate = GUI.enabled;
        GUI.enabled = false;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"), true);
        GUI.enabled = UIstate;
    }

    public static void AssetPropertyField(SerializedObject serializedObject)
    {
        EditorGUILayout.ObjectField(new GUIContent("Asset"), serializedObject.targetObject, typeof(SerializedObject), false);
    }

    public static void MinMaxSlider(string label, float minLimit, float maxLimit, ref float minValue, ref float maxValue)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(label);
        GUILayout.Space(20.0f);
        EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, minLimit, maxLimit);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUIStyle style = EditorStyles.centeredGreyMiniLabel;
        style.normal.textColor = Color.black;
        GUILayout.Label(string.Format("Min: {0} \tMax: {1}", minValue, maxValue), style);
        EditorGUILayout.EndHorizontal();
    }

    public static void MinMaxSlider(string label, int minLimit, int maxLimit, ref int minValue, ref int maxValue)
    {
        float floatMinValue = minValue;
        float floatMaxValue = maxValue;

        MinMaxSlider(label, (float)minLimit, (float)maxLimit, ref floatMinValue, ref floatMaxValue);

        minValue = Mathf.FloorToInt(floatMinValue);
        maxValue = Mathf.FloorToInt(floatMaxValue);
    }

    public static void RatiosDisplayRect(ref float[] fractions)
    {
        // Visualize All Ratios
        Rect visualizeAreaRect = EditorGUILayout.BeginHorizontal();
        float height = 25.0f;
        visualizeAreaRect.height = height;

        EditorGUI.DrawRect(visualizeAreaRect, Color.gray);

        visualizeAreaRect.x += 3.0f;
        visualizeAreaRect.width -= 6.0f;
        visualizeAreaRect.y += 3.0f;
        visualizeAreaRect.height -= 6.0f;
        float totalRectWidth = visualizeAreaRect.width;

        for (var i = 0; i < fractions.Length; ++i)
        {
            Rect rect = visualizeAreaRect;
            float fraction = fractions[i];
            float fractionWidth = totalRectWidth * fraction;

            rect.width = fractionWidth;
            EditorGUI.DrawRect(rect, m_categoryTypeColors[i] * new Color(1.0f, 1.0f, 1.0f, GUI.enabled ? 1.0f : 0.25f));

            visualizeAreaRect.x += fractionWidth;
            visualizeAreaRect.width -= fractionWidth;
        }


        EditorGUILayout.EndHorizontal();
        GUILayout.Space(height);
    }

    public static int SelectionButtonsReverse(string[] options, int selected, string label = "")
    {
        const float DarkGray = 0.4f;
        const float LightGray = 0.9f;
        const float StartSpace = 10;

        //GUILayout.Space(StartSpace);
        Color storeColor = GUI.backgroundColor;
        Color highlightCol = new Color(LightGray, LightGray, LightGray);
        Color bgCol = new Color(DarkGray, DarkGray, DarkGray);

        GUIStyle buttonStyle = new GUIStyle(EditorStyles.miniButtonMid);
        buttonStyle.normal.textColor = Color.white;
        // buttonStyle.padding.bottom = 8;

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField(label, GUILayout.Width(100.0f));

        {   //Create a row of buttons
            for (int i = 0; i < options.Length; ++i)
            {
                // Reversed
                bool selectedIndex = i == selected ? true : false;
                GUI.backgroundColor = i == selected ? bgCol : highlightCol;

                if (selectedIndex)
                    GUI.enabled = false;

                if (GUILayout.Button(options[i]/*, buttonStyle*/))
                    selected = i; //Tab click

                GUI.enabled = true;
            }
        }
        GUILayout.EndHorizontal();
        //Restore color
        GUI.backgroundColor = storeColor;
        //Draw a line over the bottom part of the buttons (ugly haxx)
        var texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, highlightCol);
        texture.Apply();
        GUI.DrawTexture(new Rect(0, buttonStyle.lineHeight + buttonStyle.border.top + buttonStyle.margin.top + StartSpace + 55, Screen.width, 4), texture);
        //GUILayout.Space(StartSpace);

        Texture2D.DestroyImmediate(texture);

        return selected;
    }

    public static int TabsMini(string[] options, int selected)
    {
        const float DarkGray = 0.4f;
        const float LightGray = 0.9f;
        const float StartSpace = 10;

        GUILayout.Space(StartSpace);
        Color storeColor = GUI.backgroundColor;
        Color highlightCol = new Color(LightGray, LightGray, LightGray);
        Color bgCol = new Color(DarkGray, DarkGray, DarkGray);

        GUIStyle buttonStyle = new GUIStyle(EditorStyles.miniButtonMid);
        buttonStyle.normal.textColor = Color.white;
        // buttonStyle.padding.bottom = 8;

        GUILayout.BeginHorizontal();
        {   //Create a row of buttons
            for (int i = 0; i < options.Length; ++i)
            {
                GUI.backgroundColor = i == selected ? highlightCol : bgCol;
                if (GUILayout.Button(options[i], buttonStyle))
                {
                    selected = i; //Tab click
                }
            }
        }
        GUILayout.EndHorizontal();
        //Restore color
        GUI.backgroundColor = storeColor;
        //Draw a line over the bottom part of the buttons (ugly haxx)
        var texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, highlightCol);
        texture.Apply();
        GUI.DrawTexture(new Rect(0, buttonStyle.lineHeight + buttonStyle.border.top + buttonStyle.margin.top + StartSpace + 55, Screen.width, 4), texture);
        GUILayout.Space(StartSpace);

        Texture2D.DestroyImmediate(texture);

        return selected;
    }
}
