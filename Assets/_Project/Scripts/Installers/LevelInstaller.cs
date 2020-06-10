using OddlySatisfying;
using Rakib;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ECSManager>().FromComponentInHierarchy().AsSingle();

        Container.DeclareSignal<PieceCollisionSignal>().OptionalSubscriber();
    }
}