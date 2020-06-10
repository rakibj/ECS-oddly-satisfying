using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Rakib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OddlySatisfying
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Slider progressSlider;
        [SerializeField] private TMP_Text currentLevelText;
        [SerializeField] private TMP_Text nextLevelText;
        
        [Inject] private SignalBus m_signalBus;
        [Inject] private StorageManager m_storageManager;

        private void Start()
        {
            currentLevelText.text = m_storageManager.CurrentLevel.ToString();
            nextLevelText.text = (m_storageManager.CurrentLevel + 1).ToString();
            progressSlider.value = 0f;
        }

        private void OnEnable()
        {
            m_signalBus.Subscribe<ProgressUpdateSignal>(OnProgress);            
        }
        private void OnDisable()
        {
            m_signalBus.Unsubscribe<ProgressUpdateSignal>(OnProgress);            
        }
        private void OnProgress(ProgressUpdateSignal progressUpdateSignal)
        {
            progressSlider.DOValue(progressUpdateSignal.Progress, 0.5f);
        }
    }
}