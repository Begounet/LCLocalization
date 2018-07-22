using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Guid SerializedProperty Helper
/// </summary>
public static class GuidSPHelper
{
    private const string InternalGuidFieldName = "_guid";

    public static void CreateAndAssignNewGuid(SerializedProperty guidProperty)
    {
        SerializedProperty internalGuidProp = guidProperty.FindPropertyRelative(InternalGuidFieldName);
        internalGuidProp.stringValue = Guid.CreateNewGuid().ToString();
    }

    public static string GetGuidValue(SerializedProperty guidProperty)
    {
        SerializedProperty internalGuidProp = guidProperty.FindPropertyRelative(InternalGuidFieldName);
        return internalGuidProp.stringValue;
    }
}
