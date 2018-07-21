using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LanguageDatatable", menuName = "LC Localization/Data table")]
public class LanguageDatatable : ScriptableObject
{  
    [SerializeField]
    public List<LanguageDatatableRow> _rows;
}
