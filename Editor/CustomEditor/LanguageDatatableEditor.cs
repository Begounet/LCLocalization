using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;

[CustomEditor(typeof(LanguageDatatable))]
public class LanguageDatatableEditor : Editor
{
    [SerializeField]
    private MultiColumnHeaderState _multiColumnHeaderState;

    [SerializeField]
    private TreeViewState _treeViewState;

    private LanguageDatatableDrawer _languageDatatableDrawer;

    void OnEnable()
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

    public override void OnInspectorGUI()
    {
        _languageDatatableDrawer.SetLanguageDatatableSerializedObject(serializedObject);
        //_languageDatatableDrawer.OnGUI(new Rect(0, 0, Screen.width, Screen.height));
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        LanguageDatatable datatable = EditorUtility.InstanceIDToObject(instanceID) as LanguageDatatable;
        if (datatable != null)
        {
            SerializedObject datatableSo = new SerializedObject(datatable);
            if (datatableSo != null)
            {
                LanguageDatatableWindow.Init(datatableSo);
                return (true);
            }
        }
        return (false);
    }

}
