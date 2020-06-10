using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace OddlySatisfying
{
    public class FollowEntity : MonoBehaviour
    {
        public Entity entityToFollow;

        private EntityManager m_entityManager;

        private void Awake()
        {
            m_entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        private void LateUpdate()
        {
            transform.position = m_entityManager.GetComponentData<Translation>(entityToFollow).Value;
            transform.rotation = m_entityManager.GetComponentData<Rotation>(entityToFollow).Value;
        }
    }
}