using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LanguageDatatable", menuName = "LC Localization/Data table")]
public class LanguageDatatable : ScriptableObject
{  
    [SerializeField]
    private List<LanguageDatatableRow> _rows;

    internal List<LanguageDatatableRow> GetRows()
    {
        return _rows;
    }
}
