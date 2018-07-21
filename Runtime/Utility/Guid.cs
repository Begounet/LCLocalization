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
}