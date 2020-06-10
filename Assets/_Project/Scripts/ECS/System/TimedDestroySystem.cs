using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace OddlySatisfying
{
    public class TimedDestroySystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var dt = Time.DeltaTime;
            Entities.WithName("TimedDestroySystem").WithoutBurst().WithStructuralChanges()
                .ForEach((ref Entity entity, ref TimedDestroyData timedDestroyData) =>
                {
                    if (timedDestroyData.RunTimer)
                    {
                        timedDestroyData.TimeToLive -= dt;
                        if(timedDestroyData.TimeToLive <= 0)
                            EntityManager.DestroyEntity(entity);
                    }
                })
                .Run();

            return inputDeps;
        }
    }
}