using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UT_Guid
{
    [Test]
    public void CreateGuid_EmptyCtor()
    {
        Guid guid = new Guid();
        Assert.IsTrue(guid.IsInvalid);
    }

    [Test]
    public void CreateGuid_NewGuid()
    {
        Guid guid = Guid.CreateNewGuid();
        Assert.IsTrue(guid.IsValid);
    }

}