using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIdentifiable
{
    Guid GetGuid();
    void SetGuid(Guid guid);
}