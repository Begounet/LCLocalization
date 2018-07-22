using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Guid
{
    [SerializeField]
    private string _guid;

    public bool IsValid
    {
        get { return !string.IsNullOrEmpty(_guid); }
    }

    public bool IsInvalid
    {
        get { return !IsValid; }
    }

    public Guid()
    {
        _guid = string.Empty;
    }

    private Guid(string guid)
    {
        _guid = guid;
    }

    public static Guid CreateNewGuid()
    {
        System.Guid guid = System.Guid.NewGuid();
        return new Guid(guid.ToString());
    }

    public static bool operator==(Guid a, Guid b)
    {
        return (a._guid == b._guid);
    }

    public static bool operator!=(Guid a, Guid b)
    {
        return (a._guid != b._guid);
    }

    public override bool Equals(object obj)
    {
        if (obj is Guid)
        {
            Guid otherGuid = obj as Guid;
            return (this == otherGuid);
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _guid.GetHashCode();
    }

    public override string ToString()
    {
        return _guid;
    }
}