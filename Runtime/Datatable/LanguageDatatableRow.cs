using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LanguageDatatableRow : IIdentifiable
{
    [SerializeField]
    public Guid _guid;

    [SerializeField]
    public List<LanguageDatatableItem> _texts;


    #region IIdentifiable implementation

    public Guid GetGuid()
    {
        return _guid;
    }

    public void SetGuid(Guid guid)
    {
        _guid = guid;
    }

    #endregion
}