using Zenject;

namespace FirebaseModul
{
    public class FirebaseInstaller : Installer<FirebaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<FirebaseController>().AsSingle().NonLazy();
        }
    }
}