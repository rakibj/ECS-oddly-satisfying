using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 RotateAngleX(this Vector3 v, float angleInRadians)
    {
        var z = v.z * Mathf.Cos(angleInRadians) - v.y * Mathf.Sin(angleInRadians);
        var y = v.z * Mathf.Sin(angleInRadians) + v.y * Mathf.Cos(angleInRadians);
        
        return new Vector3(0, y, z);
    }
}
