using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

[CustomEditor(typeof(LCLocalizationSettings))]
public class LCSettingsEditor : Editor
{
    private ReorderableList _list;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            DrawUseSettingsButton();
            DrawLanguageSettings();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawLanguageSettings()
    {
        SerializedProperty languagesProp = serializedObject.FindProperty("_languages");
        GetList(languagesProp).DoLayoutList();
    }

    private ReorderableList GetList(SerializedProperty property)
    {
        if (_list == null)
        {
            _list = new ReorderableList(serializedObject, property, true, true, true, true);
            _list.drawHeaderCallback += (Rect rect) =>
            {
                EditorGUI.LabelField(rect, new GUIContent("Languages"));
            };
            _list.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                string prefix = (index == 0) ? "Reference" : ("Aux." + index);
                rect = EditorGUI.PrefixLabel(rect, new GUIContent(prefix));
                EditorGUI.PropertyField(rect, property.GetArrayElementAtIndex(index), true);
            };
            _list.elementHeight = EditorGUIUtility.singleLineHeight + 3;
        }
        return (_list);
    }

    private void DrawUseSettingsButton()
    {
        LCLocalizationSettings settings = LocalizationManager.Instance.GetCurrentSettings();
        bool areSettingsAlreadyLoaded = (settings == target);

        if (areSettingsAlreadyLoaded)
        {
            EditorGUILayout.LabelField("Theses settings are the currently loaded.");
        }
        else
        { 
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(new GUIContent("Use these settings", "Load these settings in the LocalizationManager."), GUILayout.MaxWidth(200)))
                {
                    LocalizationManager.Instance.SetSettingsInEditor(target as LCLocalizationSettings);
                }
            }
        }

        EditorGUI.EndDisabledGroup();
    }

}
