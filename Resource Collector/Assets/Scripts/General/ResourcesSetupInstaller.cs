using UnityEngine;
using Zenject;

public class ResourcesSetupInstaller : MonoInstaller
{
    [SerializeField] ResourcesSetup _resourcesSetup;

    public override void InstallBindings()
    {
        Container.Bind<ResourcesSetup>().FromInstance(_resourcesSetup).AsSingle().NonLazy();
    }
}