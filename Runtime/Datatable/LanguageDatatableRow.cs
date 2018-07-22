using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("LCLocalization.Editor")]
[System.Serializable]
public partial class LanguageDatatableRow : IIdentifiable
{
    [SerializeField]
    internal Guid guid;

    [SerializeField]
    internal string key;

    [SerializeField]
    internal List<LanguageDatatableItem> _languageItems;

    internal LanguageDatatableItem GetLanguageText(Language language)
    {
        LCLocalizationSettings settings = LocalizationManager.Instance.GetCurrentSettings();
        int languageIndex = settings.GetLanguageIndex(language);
        if (languageIndex >= 0 && languageIndex < _languageItems.Count)
        {
            return (_languageItems[languageIndex]);
        }
        return (null);
    }

    #region IIdentifiable implementation

    public Guid GetGuid()
    {
        return guid;
    }

    public void SetGuid(Guid newGuid)
    {
        guid = newGuid;
    }

    #endregion
}