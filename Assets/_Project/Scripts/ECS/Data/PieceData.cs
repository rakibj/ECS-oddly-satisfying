using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace OddlySatisfying
{
    [GenerateAuthoringComponent]
    public struct PieceData : IComponentData
    {
        public bool TriggerOn;
        public float3 TriggerForce;
        public bool PhysicsOn;
        public float3 PhysicsForce;
    }
}