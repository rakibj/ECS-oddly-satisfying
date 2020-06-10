using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Rakib
{
    [CreateAssetMenu(menuName = "Rakib/GameConfig")]
    public class GameConfig: ScriptableObject
    {
        public GameSettings gameSettings;
        public VisualSettings visualSettings;
        public GameplayVariables gameplayVariables;
        public AvatarSettings avatarSettings;
    }

    [Serializable]
    public class GameSettings
    {
        public bool haptics;
        public bool loadLevelsAutomatically = false;
        public int totalLevels;
        public bool videoTest = true;
    }

    [Serializable]
    public class VisualSettings
    {
        public bool showLoadingUI = false;
        public bool postProcess = false;

        private void PostProcessUpdate()
        {
        }
        
    }
    
    [Serializable]
    public class GameplayVariables
    {
    }

    [Serializable]
    public class AvatarSettings
    {
        public float avatarForce = 100f;
        public float runAnimationMinSpeed = 0.5f;
        public float minScaleSpeedFactor = 0.25f;
        public Vector2 massRange = new Vector2(1f, 100f);
        public Vector2 scaleLimit = new Vector2(1f, 2f);
    }
}