using Zenject;
using UnityEngine;

public class SceneInitializer : MonoInstaller
{
    [SerializeField] private Gem _gem;
    [SerializeField] private int _gemPoolDefaultCapacity;
    [SerializeField] private int _gemPoolMaxSize;
    [SerializeField] private BaseCreator _baseCreator;
    [SerializeField] private Observer _observer;

    public override void InstallBindings()
    {
        Container.Bind<GemPool>().FromNew().AsSingle().WithArguments(_gem, _gemPoolDefaultCapacity, _gemPoolMaxSize);
        Container.Bind<BaseCreator>().FromInstance(_baseCreator).AsSingle();
        Container.Bind<Observer>().FromInstance(_observer).AsSingle();
    }
}
