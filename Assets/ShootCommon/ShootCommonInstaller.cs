using ShootCommon.AssetReferences;
using ShootCommon.InteractiveObjectsSpawnerService;
using ShootCommon.SignalSystem;
using UnityEngine.InputSystem;
using Zenject;

namespace ShootCommon
{
    public class ShootCommonInstaller: Installer<ShootCommonInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SignalService>().AsSingle();
            Container.BindInterfacesTo<AssetReferenceStorage>().AsSingle();
            Container.BindInterfacesTo<InteractiveObjectsManager>().AsSingle();
            Container.BindInterfacesTo<PlayerInput>().AsSingle();
        }
    }
}