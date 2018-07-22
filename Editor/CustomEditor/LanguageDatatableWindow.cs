using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class LanguageDatatableWindow : EditorWindow
{
    private const string LastLanguageDatatableEditedKey = "LanguageDatatableEdited";

    private SerializedObject _languageDatatableSo;

    [SerializeField]
    private MultiColumnHeaderState _multiColumnHeaderState;

    [SerializeField]
    private TreeViewState _treeViewState;

    private LanguageDatatableDrawer _languageDatatableDrawer;

    private void OnEnable()
    {
        if (EditorPrefs.HasKey(LastLanguageDatatableEditedKey)) 
        {
            string assetPath = EditorPrefs.GetString(LastLanguageDatatableEditedKey);
            LanguageDatatable languageDatatable = AssetDatabase.LoadAssetAtPath<LanguageDatatable>(assetPath);
            if (languageDatatable != null)
            {
                _languageDatatableSo = new SerializedObject(languageDatatable);
            }
        }

        if (_languageDatatableSo != null)
        { 
            if (_treeViewState == null)
            {
                _treeViewState = new TreeViewState();
            }

            if (_languageDatatableDrawer == null)
            {
                _languageDatatableDrawer = new LanguageDatatableDrawer(_treeViewState, CreateMultiColumnHeader());
            }
        }
    }

    private MultiColumnHeader CreateMultiColumnHeader()
    {
        bool isFirstInit = _multiColumnHeaderState == null;

        var headerState = new MultiColumnHeaderState(LanguageDatatableDrawer.CreateColumnHeaders());
        if (MultiColumnHeaderState.CanOverwriteSerializedFields(_multiColumnHeaderState, headerState))
        {
            MultiColumnHeaderState.OverwriteSerializedFields(_multiColumnHeaderState, headerState);
        }
        _multiColumnHeaderState = headerState;

        MultiColumnHeader multiColumnHeader = new MultiColumnHeader(_multiColumnHeaderState);
        if (isFirstInit)
        {
            multiColumnHeader.ResizeToFit();
        }
        return (multiColumnHeader);
    }

    public static void Init(SerializedObject languageDatatableSo)
    {
        LanguageDatatableWindow window = EditorWindow.GetWindow<LanguageDatatableWindow>(languageDatatableSo.targetObject.name);
        EditorPrefs.SetString(LastLanguageDatatableEditedKey, AssetDatabase.GetAssetPath(languageDatatableSo.targetObject));
        window._languageDatatableSo = languageDatatableSo;
        window.Show();
    }

    private void OnGUI()
    {
        _languageDatatableDrawer.SetLanguageDatatableSerializedObject(_languageDatatableSo);

        EditorGUI.BeginChangeCheck();
        {
            _languageDatatableDrawer.OnGUI(new Rect(0, 0, position.width, position.height));
        }
        if (EditorGUI.EndChangeCheck())
        {
            _languageDatatableSo.ApplyModifiedProperties();
        }
    }
}
