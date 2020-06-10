using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
using Zenject;

namespace OddlySatisfying
{
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    public class PlayerCollisionSystem : JobComponentSystem
    {
        private BuildPhysicsWorld m_buildPhysicsWorld;
        private StepPhysicsWorld m_stepPhysicsWorld;

        protected override void OnCreate()
        {
            m_buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            m_stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        }

        struct PieceCollisionEvent: IComponentData{}
        struct PlayerCollisionJob : ICollisionEventsJob
        {
            public ComponentDataFromEntity<PlayerData> PlayerGroup;
            [ReadOnly]public ComponentDataFromEntity<ObstacleData> ObstacleGroup;
            public ComponentDataFromEntity<PieceData> PieceGroup;
            
            public void Execute(CollisionEvent collisionEvent)
            {
                var entityA = collisionEvent.Entities.EntityA;
                var entityB = collisionEvent.Entities.EntityB;


                if ((PlayerGroup.Exists(entityA) && PieceGroup.Exists(entityB)) ||
                    (PlayerGroup.Exists(entityB) && PieceGroup.Exists(entityA)))
                {
                    //var playerEntity = PlayerGroup.Exists(entityA) ? entityA : entityB;
                    var pieceEntity = PieceGroup.Exists(entityA) ? entityA : entityB;

                    var pieceData = PieceGroup[pieceEntity];
                    pieceData.TriggerOn = true;
                    pieceData.PhysicsOn = true;
                    PieceGroup[pieceEntity] = pieceData;
                }
                
                if ((PlayerGroup.Exists(entityA) && ObstacleGroup.Exists(entityB)) ||
                    (PlayerGroup.Exists(entityB) && ObstacleGroup.Exists(entityA)))
                {
                    var playerEntity = PlayerGroup.Exists(entityA) ? entityA : entityB;

                    var playerData = PlayerGroup[playerEntity];
                    playerData.Death = true;
                    PlayerGroup[playerEntity] = playerData;
                }
            }
        }
        
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var jobHandle = new PlayerCollisionJob
            {
                PlayerGroup = GetComponentDataFromEntity<PlayerData>(),
                PieceGroup = GetComponentDataFromEntity<PieceData>(),
                ObstacleGroup = GetComponentDataFromEntity<ObstacleData>(),
            }.Schedule(m_stepPhysicsWorld.Simulation, ref m_buildPhysicsWorld.PhysicsWorld, inputDeps);
            
            jobHandle.Complete();
            return inputDeps;
        }

    }
}