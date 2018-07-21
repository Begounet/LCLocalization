using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LanguageDatatableWindow : EditorWindow
{
    private SerializedObject _languageDatatableSo;

    public static void Init(SerializedObject languageDatatableSo)
    {
        LanguageDatatableWindow window = EditorWindow.GetWindow<LanguageDatatableWindow>();
        window._languageDatatableSo = languageDatatableSo;
        window.Show();
    }

    private void OnGUI()
    {
        LanguageDatatableDrawer.Draw(_languageDatatableSo);
    }
}
