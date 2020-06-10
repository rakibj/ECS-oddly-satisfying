
using UnityEngine;
using UnityEngine.Analytics;
using Zenject;

namespace Rakib
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        public GameConfig gameConfig;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.BindInstance(gameConfig);
            Container.Bind<StorageManager>().FromInstance(new StorageManager()).AsSingle();
            Container.Bind<AnalyticsManager>().FromInstance(new AnalyticsManager()).AsSingle();
            
            //Signals
            Container.DeclareSignal<LevelLoadSignal>();
            Container.DeclareSignal<LevelStartSignal>();
            Container.DeclareSignal<LevelCompleteSignal>();
            Container.DeclareSignal<LevelFailSignal>();
            Container.DeclareSignal<LevelLoadNextSignal>();
            Container.DeclareSignal<LevelLoadSameSignal>();
            Container.DeclareSignal<ProgressUpdateSignal>().OptionalSubscriber();
        }
    }
}