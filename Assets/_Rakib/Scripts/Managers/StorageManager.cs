using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Rakib
{
    public class StorageManager
    {
        private const string LEVEL_KEY = "LEVELKEY";
        private const string GAMEDATA_KEY = "GAMEDATAKEY";
        
        public bool GameDataAvailable => PlayerPrefs.HasKey(GAMEDATA_KEY);
        
        public void SaveGameData(string jsonString)
        {
            PlayerPrefs.SetString(GAMEDATA_KEY, jsonString);
                
        }
        public string LoadGameData()
        {
            if (!PlayerPrefs.HasKey(GAMEDATA_KEY)) return null;
            return PlayerPrefs.GetString(GAMEDATA_KEY);
        }
        
        public int CurrentLevel
        {
            get => LoadOrCreateKeyInt(LEVEL_KEY, 1);
            set => PlayerPrefs.SetInt(LEVEL_KEY, value);
        }

        private int LoadOrCreateKeyInt(string key, int defaultValue = 1)
        {
            if (PlayerPrefs.HasKey(key))
                return PlayerPrefs.GetInt(key);
            else
            {
                PlayerPrefs.SetInt(key, defaultValue);
                return defaultValue;
            }
        }
        
    }
}