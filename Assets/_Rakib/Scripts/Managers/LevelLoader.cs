using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Rakib
{
    public class LevelLoader : MonoBehaviour
    {
        private GameConfig m_gameConfig;
        private SignalBus m_signalBus;
        private StorageManager m_storageManager;
        private int m_currentLevel;
        
        [Inject]
        private void Construct(GameConfig gameConfig, SignalBus signalBus, StorageManager storageManager)
        {
            m_gameConfig = gameConfig;
            m_signalBus = signalBus;
            m_storageManager = storageManager;
        }
        private void OnEnable()
        {
            m_signalBus.Subscribe<LevelLoadNextSignal>(LevelComplete);
        }

        private void OnDisable()
        {
            m_signalBus.Unsubscribe<LevelLoadNextSignal>(LevelComplete);
        }

        private void Start()
        {
            if (!m_gameConfig.gameSettings.loadLevelsAutomatically)
                return;

            m_currentLevel = m_storageManager.CurrentLevel;
            LoadNextLevel(m_currentLevel);
        }


        private void LevelComplete(LevelLoadNextSignal signal)
        {
            if (!m_gameConfig.gameSettings.loadLevelsAutomatically)
            {
                LoadNextLevel(m_storageManager.CurrentLevel);
                return;
            }
            m_storageManager.CurrentLevel++;
            LoadNextLevel(m_storageManager.CurrentLevel);
        }
        
        private void LoadNextLevel(int levelToLoad)
        {
            
            if (levelToLoad > m_gameConfig.gameSettings.totalLevels)
                levelToLoad = Random.Range(1, m_gameConfig.gameSettings.totalLevels + 1);

            SceneManager.LoadScene("Level_" + levelToLoad, LoadSceneMode.Single);
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        }
    }
}