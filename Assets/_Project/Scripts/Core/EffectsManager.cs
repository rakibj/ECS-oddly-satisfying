using System;
using System.Collections;
using System.Collections.Generic;
using Rakib;
using UnityEngine;
using Zenject;

namespace Rakib
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem winEffects;
        [Inject] private SignalBus m_signalBus;

        private void OnEnable()
        {
            m_signalBus.Subscribe<LevelCompleteSignal>(PlayWinEffects);
        }

        private void OnDisable()
        {
            m_signalBus.Unsubscribe<LevelCompleteSignal>(PlayWinEffects);
        }

        private void PlayWinEffects()
        {
            winEffects.Play(true);
        }
    }
}