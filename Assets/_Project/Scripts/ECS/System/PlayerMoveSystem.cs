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
        private float m_moveSpeed = 5f;
        private float m_slideTolerance = 0;
        private float m_moveFactor;
        private Vector3 m_moveDirection;
        private Vector3 m_previousTouchPosition;
        private float m_slideSensitivity = 0.8f;
        private float m_xLimit = 5f;

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
//                        var distance = m_previousTouchPosition.x - tapPosition.x;
//                        m_moveFactor = (math.abs(distance) > m_slideTolerance) ? distance : 0;
//                        m_previousTouchPosition = tapPosition;

                        var direction = m_previousTouchPosition - tapPosition;
                        direction = direction.normalized;
                        m_previousTouchPosition = tapPosition;
                        m_moveDirection = new Vector3(direction.x, 0, direction.y);
                    }

                    if (tapUp)
                    {
//                        m_moveFactor = 0;
                        m_moveDirection = Vector3.zero;
                    }
                    
//                    var moveX = m_moveFactor;
//                    var position = translation.Value;
//
//                    var sideMove = dt * m_slideSensitivity * moveX;
//
//                    var sideMovePosition = sideMove + position.x;
//                    if (sideMovePosition > m_xLimit || sideMovePosition < -m_xLimit) sideMove = 0f;

                    //var forwardMove = m_moveSpeed * dt;
                    //translation.Value += new float3(0,0,forwardMove);
                    
//                    physicsVelocity.Linear = new float3(0,0,m_moveSpeed);
//                    
//                    translation.Value += new float3(-sideMove,0,0);

                    physicsVelocity.Linear = new float3(m_moveDirection.x, m_moveDirection.y, m_moveDirection.z) * -10f;
                    rotation.Value = math.mul(rotation.Value, quaternion.RotateY(2));
                    //scale.Value += new float3(-sideMove,0,0);
                })
                .Run();
            
            return inputDeps;
        }
    }
}