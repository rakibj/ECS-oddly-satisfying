using System;
using System.Collections;
using System.Collections.Generic;
using Rakib;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using Zenject;

namespace OddlySatisfying
{
    //[UpdateAfter(typeof(FreezePositionSystem))]
    public class PieceSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            
            var jobHandle = Entities.WithName("PieceSystem")
                .ForEach((ref PhysicsMass physicsMass, ref PhysicsVelocity physicsVelocity, ref PieceData pieceData, ref TimedDestroyData timedDestroyData) =>
                {
                    if (pieceData.TriggerOn)
                    {
                        //ecsManager.InvokeCollisionPiece();
                        timedDestroyData.RunTimer = true;
                        physicsVelocity.Linear = pieceData.TriggerForce; //new float3(0f, 5f, 0f);
                        pieceData.TriggerOn = false;
                    }

                    if (pieceData.PhysicsOn)
                    {
                        physicsVelocity.Linear += pieceData.PhysicsForce; //new float3(0, 0f, -0.5f);
                    }
                })
                .Schedule(inputDeps);

            jobHandle.Complete();
            
            return inputDeps;
        }
    }
}