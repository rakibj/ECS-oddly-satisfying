using System;
using System.Collections;
using System.Collections.Generic;
using Rakib;
using Unity.Entities;
using UnityEngine;
using Zenject;

namespace OddlySatisfying
{
    public class ECSManager : MonoBehaviour
    {
        private World m_world;
        private EntityManager m_entityManager;
        private int m_totalPieces;

        [Inject] private SignalBus m_signalBus;
        
        [SerializeField]private int currentPieces;
        [SerializeField] private float completedPercentage;

        private EntityQuery m_piecesQuery;
        private bool m_gameRunning;
        
        private void Start()
        {
            m_signalBus.Fire(new LevelStartSignal());
            m_gameRunning = true;
            
            m_world = World.DefaultGameObjectInjectionWorld;
            m_entityManager = m_world.EntityManager;
            m_piecesQuery = m_entityManager.CreateEntityQuery(ComponentType.ReadOnly<PieceData>());
            m_totalPieces = m_piecesQuery.CalculateEntityCount();
        }

        private void Update()
        {
            if (!m_gameRunning) return;
            currentPieces = m_piecesQuery.CalculateEntityCount();
            completedPercentage = 1- ((float) currentPieces / (float)m_totalPieces);
            m_signalBus.Fire(new ProgressUpdateSignal{Progress = completedPercentage});
            if (completedPercentage >= 1f)
            {
                m_gameRunning = false;
                m_signalBus.Fire(new LevelCompleteSignal());
            }
        }
    }
}