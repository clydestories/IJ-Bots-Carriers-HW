using Zenject;
using UnityEngine;

public class SceneInitializer : MonoInstaller
{
    [SerializeField] private Gem _gem;
    [SerializeField] private int _gemPoolDefaultCapacity;
    [SerializeField] private int _gemPoolMaxSize;

    public override void InstallBindings()
    {
        Container.Bind<GemPool>().FromNew().AsSingle().WithArguments(_gem, _gemPoolDefaultCapacity, _gemPoolMaxSize);
    }
}
