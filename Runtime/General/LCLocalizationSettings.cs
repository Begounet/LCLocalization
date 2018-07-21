using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LC-Settings", menuName = "LC Localization/Settings")]
public class LCLocalizationSettings : ScriptableObject
{
    [SerializeField]
    private List<Language> _languages = new List<Language>(new Language[] { new Language("English") });
}
