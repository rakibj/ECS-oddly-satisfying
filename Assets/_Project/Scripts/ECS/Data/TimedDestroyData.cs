using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace OddlySatisfying
{
    [GenerateAuthoringComponent]
    public struct TimedDestroyData : IComponentData
    {
        public bool RunTimer;
        public float TimeToLive;
    }
}