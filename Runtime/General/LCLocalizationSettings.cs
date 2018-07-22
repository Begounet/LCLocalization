using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LC-Settings", menuName = "LC Localization/Settings")]
public class LCLocalizationSettings : ScriptableObject
{
    [SerializeField]
    private List<Language> _languages = new List<Language>(new Language[] { new Language("English") });

    public int LanguagesCount
    {
        get { return _languages.Count; }
    }

    public IEnumerable<Language> GetLanguages()
    {
        return _languages;
    }

    internal int GetLanguageIndex(Language language)
    {
        return _languages.IndexOf(language);
    }
}
