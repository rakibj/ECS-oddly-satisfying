using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace OddlySatisfying
{
    [RequiresEntityConversion]
    public class LeaderAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public GameObject follower;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var followEntity = follower.GetComponent<FollowEntity>();
            if (followEntity == null)
                followEntity = follower.AddComponent<FollowEntity>();

            followEntity.entityToFollow = entity;
        }
    }
}