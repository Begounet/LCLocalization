#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

public partial class LanguageDatatableRow : TreeViewItem
{
    public override int CompareTo(TreeViewItem other)
    {
        LanguageDatatableRow otherRow = other as LanguageDatatableRow;
        if (otherRow == null)
        {
            return (1);
        }

        return (this.guid.GetHashCode() - otherRow.guid.GetHashCode());
    }
}

#endif