using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class LocalizationManagerEditor
{
    static LocalizationManagerEditor()
    {
        CreateLocalizationManagerInstance();
    }

    private static void CreateLocalizationManagerInstance()
    {
        LocalizationManager.Initialize();
    }

}
