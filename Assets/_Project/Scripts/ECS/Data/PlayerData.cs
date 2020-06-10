using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct PlayerData : IComponentData
{
    public float RotationSpeed;
    public float MoveSensitivity;
    public bool Death;
}
