using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.IMGUI.Controls;
using System;
using UnityEditor.TreeViewExamples;
using System.Linq;

public class LanguageDatatableDrawer : TreeView
{
    private static SerializedObject _languageDatatableSo;

    public LanguageDatatableDrawer(TreeViewState treeViewState, MultiColumnHeader multiColumnHeader) 
        : base(treeViewState, multiColumnHeader)
    {
        rowHeight = EditorGUIUtility.singleLineHeight + 2 * EditorGUIUtility.standardVerticalSpacing;
        showAlternatingRowBackgrounds = true;
        showBorder = true;
    }

    public static MultiColumnHeaderState.Column[] CreateColumnHeaders()
    {
        LCLocalizationSettings settings = LocalizationManager.Instance.GetCurrentSettings();
        MultiColumnHeaderState.Column[] columns = new MultiColumnHeaderState.Column[settings.LanguagesCount + 1];

        int i = 0;
        columns[i++] = new MultiColumnHeaderState.Column()
        {
            headerContent = new GUIContent("Key"),
            autoResize = true,
            allowToggleVisibility = false,
            canSort = true,
        };

        foreach (Language language in settings.GetLanguages())
        {
            columns[i++] = new MultiColumnHeaderState.Column()
            {
                headerContent = new GUIContent(language.Name),                
                autoResize = true,
                allowToggleVisibility = true,
                canSort = false,
            };
        }

        return (columns);
    }

    public void SetLanguageDatatableSerializedObject(SerializedObject languageDatatableSo)
    {
        _languageDatatableSo = languageDatatableSo;
        Reload();
    }

    protected override TreeViewItem BuildRoot()
    {
        var root = new TreeViewItem(0, -1, "Root");
        return (root);
    }

    protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
    {
        List<TreeViewItem> treeViewItems = new List<TreeViewItem>();

        if (_languageDatatableSo != null)
        {
            LanguageDatatable languageDatatable = _languageDatatableSo.targetObject as LanguageDatatable;
            List<LanguageDatatableRow> rows = languageDatatable.GetRows();

            for (int i = 0; i < rows.Count; ++i)
            {
                rows[i].id = i;
                rows[i].depth = 0;
                rows[i].displayName = rows[i].key;
                root.AddChild(rows[i]);
                treeViewItems.Add(rows[i]);
            }
        }

        return (treeViewItems);
    }

    protected override bool CanStartDrag(CanStartDragArgs args)
    {
        // TODO : Implement drag
        return false;
    }

    private const string DatatableDragID = "LanguageDatatableDragColumnDragging";
    protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
    {
        DragAndDrop.PrepareStartDrag();
        var draggedRows = GetRows().Where(item => args.draggedItemIDs.Contains(item.id)).ToList();
        DragAndDrop.SetGenericData(DatatableDragID, draggedRows);
        DragAndDrop.objectReferences = new UnityEngine.Object[] { }; // this IS required for dragging to work
        string title = draggedRows.Count == 1 ? draggedRows[0].displayName : "< Multiple >";
        DragAndDrop.StartDrag(title);
    }

    protected override DragAndDropVisualMode HandleDragAndDrop(DragAndDropArgs args)
    {
        // Check if we can handle the current drag data (could be dragged in from other areas/windows in the editor)
        var draggedRows = DragAndDrop.GetGenericData(DatatableDragID) as List<TreeViewItem>;
        if (draggedRows == null)
        {
            return DragAndDropVisualMode.None;
        }

        switch (args.dragAndDropPosition)
        {
            case TreeView.DragAndDropPosition.BetweenItems:
                if (args.performDrop)
                {
                    MoveLines(draggedRows, args.insertAtIndex);
                }
                return DragAndDropVisualMode.Move;
        }
        return DragAndDropVisualMode.None;
    }

    protected override bool CanMultiSelect(TreeViewItem item)
    {
        return true;
    }

    protected override void RowGUI(RowGUIArgs args)
    {
        SerializedProperty rowsProp = GetRowsProp();
        SerializedProperty currentRowProp = rowsProp.GetArrayElementAtIndex(args.row);

        Rect keyRect = args.GetCellRect(args.GetColumn(0));
        EditorGUI.LabelField(new Rect(keyRect.x, keyRect.y, 10, keyRect.height), args.item.id.ToString());
        AddHorizontalPadding(ref keyRect, 2);
        AddHorizontalPadding(ref keyRect, 10, 0);
        CenterRectUsingSingleLineHeight(ref keyRect);
        SerializedProperty keyProp = GetLanguageRowKeyProp(currentRowProp);
        EditorGUI.DelayedTextField(keyRect, keyProp, GUIContent.none);

        LCLocalizationSettings settings = LocalizationManager.Instance.GetCurrentSettings();
        for (int i = 0; i < settings.LanguagesCount; ++i)
        {
            SerializedProperty languageItemsProp = GetLanguageItemsProp(currentRowProp);
            SerializedProperty textProp = GetLanguageTextProp(languageItemsProp.GetArrayElementAtIndex(i));

            Rect textRect = args.GetCellRect(args.GetColumn(i + 1));
            AddHorizontalPadding(ref textRect, 2);
            CenterRectUsingSingleLineHeight(ref textRect);
            textProp.stringValue = EditorGUI.TextArea(textRect, textProp.stringValue);
        }
    }

    public override void OnGUI(Rect rect)
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            float buttonsHeight = EditorGUIUtility.singleLineHeight;

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Add line", GUILayout.MaxWidth(100), GUILayout.Height(buttonsHeight)))
            {
                AddNewLine();
            }
            if (GUILayout.Button("Clear", GUILayout.MaxWidth(100), GUILayout.Height(buttonsHeight)))
            {
                if (EditorUtility.DisplayDialog("Are you sure?", "Every line will be permanently deleted.\nAre you sure to processed?", "Delete", "Cancel"))
                {
                    SerializedProperty rowsProp = GetRowsProp();
                    rowsProp.arraySize = 0;
                    _languageDatatableSo.ApplyModifiedProperties();
                }
            }
        }

        rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        base.OnGUI(rect);
    }

    SerializedProperty  GetRowsProp()
    {
        return _languageDatatableSo.FindProperty("_rows");
    }

    SerializedProperty  GetLanguageItemsProp(SerializedProperty rowProp)
    {
        return rowProp.FindPropertyRelative("_languageItems");
    }

    SerializedProperty  GetLanguageTextProp(SerializedProperty languageItemProp)
    {
        return languageItemProp.FindPropertyRelative("text");
    }

    SerializedProperty GetLanguageRowKeyProp(SerializedProperty rowProp)
    {
        return rowProp.FindPropertyRelative("key");
    }

    SerializedProperty GetGuidProp(SerializedProperty prop)
    {
        return (prop.FindPropertyRelative("guid"));
    }

    void AddNewLine()
    {
        SerializedProperty rowsProp = GetRowsProp();
        int newItemIndex = rowsProp.arraySize;
        rowsProp.InsertArrayElementAtIndex(newItemIndex);
        SerializedProperty newRowProp = rowsProp.GetArrayElementAtIndex(newItemIndex);

        SerializedProperty newKeyProp = GetLanguageRowKeyProp(newRowProp);
        newKeyProp.stringValue = "None";

        SerializedProperty guidProp = GetGuidProp(newRowProp);
        GuidSPHelper.CreateAndAssignNewGuid(guidProp);

        LCLocalizationSettings settings = LocalizationManager.Instance.GetCurrentSettings();
        SerializedProperty languageItemsProp = GetLanguageItemsProp(newRowProp);
        languageItemsProp.arraySize = settings.LanguagesCount;

        _languageDatatableSo.ApplyModifiedProperties();
    }

    private void AddHorizontalPadding(ref Rect rect, float margin)
    {
        rect.x += margin;
        rect.width -= margin * 2;
    }

    private void AddHorizontalPadding(ref Rect rect, float left, float right)
    {
        rect.x += left;
        rect.width -= right + left;
    }

    private void MoveLines(List<TreeViewItem> draggedRows, int insertAtIndex)
    {
        List<int> selectionIndexes = new List<int>();

        SerializedProperty rowsProp = GetRowsProp();
        for (int i = 0; i < draggedRows.Count; ++i)
        {
            int foundIndex = FindItemIndexInSerializedArray(rowsProp, draggedRows[i]);
            if (foundIndex != -1)
            {
                int offset = 0;
                if (insertAtIndex > foundIndex)
                {
                    offset = -1;
                }
                //rowsProp.MoveArrayElement(foundIndex, insertAtIndex - offset);
                //selectionIndexes.Add(insertAtIndex - draggedRows.Count + i);
            }
        }

        _languageDatatableSo.ApplyModifiedProperties();
        SetSelection(selectionIndexes, TreeViewSelectionOptions.RevealAndFrame);
        Reload();
    }

    private int FindItemIndexInSerializedArray(SerializedProperty rowsProp, TreeViewItem rowItem)
    {
        LanguageDatatableRow lgDtRow = rowItem as LanguageDatatableRow;
        string referenceGuid = lgDtRow.GetGuid().ToString();

        for (int i = 0; i < rowsProp.arraySize; ++i)
        { 
            SerializedProperty rowProp = rowsProp.GetArrayElementAtIndex(i);
            string rowGuid = GuidSPHelper.GetGuidValue(GetGuidProp(rowProp));
            if (referenceGuid == rowGuid)
            {
                return (i);
            }
        }
        return (-1);
    }

}
