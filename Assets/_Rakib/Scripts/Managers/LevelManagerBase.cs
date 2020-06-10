using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Rakib
{
    public class LevelManagerBase : MonoBehaviour
    {
        [Inject] protected SignalBus SignalBus;
        [Inject] protected StorageManager m_storageManager;
        
        [Tooltip("Used to automatically fire LevelStart Signal")]
        [SerializeField] private bool testingMode = false;
        [SerializeField] private GameState m_gameState;
        
        protected virtual void Awake()
        {
            LevelLoad();
        }

        protected virtual void OnEnable()
        {
            SignalBus.Subscribe<LevelLoadNextSignal>(LevelLoad);
            SignalBus.Subscribe<LevelStartSignal>(LevelStart);
            SignalBus.Subscribe<LevelCompleteSignal>(LevelComplete);
            SignalBus.Subscribe<LevelFailSignal>(LevelFail);
        }

        protected virtual void OnDisable()
        {
            SignalBus.Unsubscribe<LevelLoadNextSignal>(LevelLoad);
            SignalBus.Unsubscribe<LevelStartSignal>(LevelStart);
            SignalBus.Unsubscribe<LevelCompleteSignal>(LevelComplete);
            SignalBus.Unsubscribe<LevelFailSignal>(LevelFail);
        }

        protected virtual void LevelLoad()
        {
            m_gameState = GameState.WaitingToStart;
            SignalBus.Fire(new LevelLoadSignal());
            if (testingMode)
            {
                SignalBus.Fire(new LevelStartSignal());
            }
        }

        /// <summary>
        /// Called when LevelStartSignal is fired
        /// </summary>
        protected virtual void LevelStart()
        {
            m_gameState = GameState.Running;
        }

        /// <summary>
        /// Called when LevelCompleteSignal is fired
        /// </summary>
        private protected virtual void LevelComplete()
        {
            m_gameState = GameState.Complete;
        }
        /// <summary>
        /// Called when LevelFailSignal is fired
        /// </summary>
        protected virtual void LevelFail()
        {
            m_gameState = GameState.Fail;
        }

        /// <summary>
        /// Call this to fire the LevelLoadNextSignal and load the next level
        /// </summary>
        private protected virtual void Debug_LoadNextLevel()
        {
            SignalBus.Fire(new LevelLoadNextSignal());
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E)) Debug_LoadNextLevel();
        }
    }
    
    public enum GameState
    {
        WaitingToStart,
        Running,
        Complete,
        Fail
    }
}