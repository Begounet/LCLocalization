using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Language : IIdentifiable
{
    [SerializeField]
    private Guid _guid;

    [SerializeField]
    private string _name;

    public string Name
    {
        get
        {
            return _name;
        }

        internal set
        {
            _name = value;
        }
    }

    public Language(string name)
    {
        SetGuid(Guid.CreateNewGuid());
        _name = name;
    }

    public Guid GetGuid()
    {
        return _guid;
    }

    public void SetGuid(Guid guid)
    {
        _guid = guid;
    }
}
