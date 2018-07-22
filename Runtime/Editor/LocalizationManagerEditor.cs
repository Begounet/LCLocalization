#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class LocalizationManager
{
    internal void SetSettingsInEditor(LCLocalizationSettings settings)
    {
        if (_core.GetSettings() != settings)
        {
            SetSettings(settings);
            EditorUtility.SetDirty(_core);
        }
    }
}

#endif // UNITY_EDITOR