using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace OddlySatisfying
{
    public class PlayerMoveSystem : JobComponentSystem
    {
        private float m_moveFactor;
        private Vector3 m_moveDirection;
        private Vector3 m_previousTouchPosition;

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var tapDown = Input.GetMouseButtonDown(0);
            var tap = Input.GetMouseButton(0);
            var tapUp = Input.GetMouseButtonUp(0);
            var tapPosition = Input.mousePosition;
            var dt = Time.DeltaTime;
            
            Entities.WithName("PlayerMoveSystem").WithoutBurst()
                .ForEach((ref PlayerData playerData, ref Translation translation, ref PhysicsVelocity physicsVelocity, ref Rotation rotation) => //, ref NonUniformScale scale
                {
                    if (playerData.Death)
                    {
                        physicsVelocity.Linear = float3.zero;
                        return;
                    }
                    
                    if (tapDown)
                    {
                        m_previousTouchPosition = tapPosition;
                    }
                    if (tap)
                    {
                        var direction = m_previousTouchPosition - tapPosition;
                        direction = direction.normalized;
                        m_previousTouchPosition = tapPosition;
                        m_moveDirection = new Vector3(direction.x, 0, direction.y);
                    }

                    if (tapUp)
                    {
                        m_moveDirection = Vector3.zero;
                    }
                    
                    physicsVelocity.Linear = new float3(m_moveDirection.x, m_moveDirection.y, m_moveDirection.z) * -1 * playerData.MoveSensitivity;
                    rotation.Value = math.mul(rotation.Value, quaternion.AxisAngle(new float3(0,1,0), playerData.RotationSpeed * dt));
                })
                .Run();
            
            return inputDeps;
        }
    }
}