using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("LCLocalization.Editor")]
public class LocalizationManager
{
    public event Action OnSettingsChanged;

    private const string LCCoreResourceName = "LC-Core";

    #region Singleton instance
    private static LocalizationManager _instance;
    public static LocalizationManager Instance
    {
        get { return _instance; }
    }
    #endregion

    private LCCore _core;

    [RuntimeInitializeOnLoadMethod]
    internal static void Initialize()
    {
        if (_instance == null)
        {
            _instance = new LocalizationManager();
            Debug.Log("LC LocalizationManager created");
        }
    }

    private LocalizationManager()
    {
        _core = Resources.Load<LCCore>(LCCoreResourceName);
        if (_core == null)
        {
            Debug.LogErrorFormat(
                "Cannot load the LC Core! Without it, nothing will work." +
                "Check that '{0}' is inside a Resources directory, " +
                "or create a new one (Assets/Create/LC Localization/Internals/Core.", LCCoreResourceName);
        }
    }

    public LCLocalizationSettings   GetCurrentSettings()
    {
        return _core.GetSettings();
    }

    public void SetSettings(LCLocalizationSettings settings)
    {
        if (_core.GetSettings() != settings)
        {
            _core.SetSettings(settings);

            if (OnSettingsChanged != null)
            {
                OnSettingsChanged.Invoke();
            }
        }
    }
}
