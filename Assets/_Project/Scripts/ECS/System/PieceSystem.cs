using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

namespace OddlySatisfying
{
    //[UpdateAfter(typeof(FreezePositionSystem))]
    public class PieceSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var jobHandle = Entities.WithName("PieceSystem")
                .ForEach((ref PhysicsMass physicsMass, ref PhysicsVelocity physicsVelocity, ref PieceData pieceData) =>
                {
                    if (pieceData.TriggerForce)
                    {
                        physicsVelocity.Linear = new float3(0f, 5f, 0f);
                        pieceData.TriggerForce = false;
                    }

                    if (pieceData.PhysicsOn)
                    {
                        physicsVelocity.Linear += new float3(0, 0f, -0.5f);
                    }
                })
                .Schedule(inputDeps);

            jobHandle.Complete();
            return inputDeps;
        }
    }
}