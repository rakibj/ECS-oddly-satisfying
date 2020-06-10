using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace OddlySatisfying
{
    [GenerateAuthoringComponent]
    public struct PieceData : IComponentData
    {
        public bool TriggerForce;
        public bool PhysicsOn;
    }
}