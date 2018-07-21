using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Language))]
public class LanguagePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(UnityEngine.Rect position, UnityEditor.SerializedProperty property, UnityEngine.GUIContent label)
    {
        SerializedProperty nameProp = property.FindPropertyRelative("_name");
        nameProp.stringValue = EditorGUI.TextField(position, nameProp.stringValue);
    }

    public override float GetPropertyHeight(UnityEditor.SerializedProperty property, UnityEngine.GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
