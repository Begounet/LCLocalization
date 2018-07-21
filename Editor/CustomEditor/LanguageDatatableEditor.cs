using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LanguageDatatable))]
public class LanguageDatatableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            LanguageDatatableDrawer.Draw(serializedObject);
            DrawOpenWindowButton();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawOpenWindowButton()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open..."))
            {
                LanguageDatatableWindow.Init(serializedObject);
            }
        }
    }

}
