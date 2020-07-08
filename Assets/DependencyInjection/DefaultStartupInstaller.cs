using Interfaces;
using Services;
using Zenject;

namespace DependencyInjection
{
    public class DefaultStartupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlatformManager>().To<PlatformManager>().AsSingle();
        }
    }
}