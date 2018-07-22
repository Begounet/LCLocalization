using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define the core data of the LC Localization system.
/// Should stay with the same name in the Resources directory
/// to be loaded correctly.
/// </summary>
[CreateAssetMenu(fileName = "LC-Core", menuName = "LC Localization/Internal/Core")]
public class LCCore : ScriptableObject
{
    //[HideInInspector]
    [SerializeField]
    public LCLocalizationSettings _settings;

    /// <summary>
    /// Use the LocalizationManager to set the settings
    /// </summary>
    internal void SetSettings(LCLocalizationSettings newSettings)
    {
        _settings = newSettings;
    }

    public LCLocalizationSettings GetSettings()
    {
        return (_settings);
    }
}
