using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Rakib
{
    public class GameManager : MonoBehaviour
    {
        private SignalBus m_signalBus;
        private AnalyticsManager m_analyticsManager;
        private StorageManager m_storageManager;
        private GameConfig m_gameConfig;
        private int m_runningLevel;

        [Inject]
        private void Construct(SignalBus signalBus, AnalyticsManager analyticsManager, StorageManager storageManager,
            GameConfig gameConfig)
        {
            m_signalBus = signalBus;
            m_analyticsManager = analyticsManager;
            m_storageManager = storageManager;
            m_gameConfig = gameConfig;
        }

        private void Awake()
        {
            m_analyticsManager.Initialize();
        }

        private void OnEnable()
        {
            m_signalBus.Subscribe<LevelLoadSignal>(LevelLoaded);
            m_signalBus.Subscribe<LevelStartSignal>(LevelStarted);
            m_signalBus.Subscribe<LevelCompleteSignal>(LevelComplete);
            m_signalBus.Subscribe<LevelFailSignal>(LevelFail);
        }

        private void OnDisable()
        {
            m_signalBus.Unsubscribe<LevelLoadSignal>(LevelLoaded);
            m_signalBus.Unsubscribe<LevelStartSignal>(LevelStarted);
            m_signalBus.Unsubscribe<LevelCompleteSignal>(LevelComplete);
            m_signalBus.Unsubscribe<LevelFailSignal>(LevelFail);
        }
        private void LevelLoaded(LevelLoadSignal signal)
        {
            Debug.Log("GameManager: Level Loaded");
        }
        
        private void LevelStarted(LevelStartSignal signal)
        {
            Debug.Log("GameManager: Level Started");
            m_runningLevel = m_storageManager.CurrentLevel;
            m_analyticsManager.LevelStarted(m_runningLevel);
            
        }

        private void LevelComplete(LevelCompleteSignal signal)
        {
            Debug.Log("GameManager: Level Complete");
            m_analyticsManager.LevelComplete(m_runningLevel);
            if (m_gameConfig.gameSettings.videoTest) return;
        }
        private void LevelFail(LevelFailSignal signal)
        {
            Debug.Log("GameManager: Level Fail");
            m_analyticsManager.LevelFail(m_runningLevel);
        }
        
    }
}