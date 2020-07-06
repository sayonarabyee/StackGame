using Interfaces;
using Services;
using Zenject;

namespace DependencyInjection
{
    public class DefaultStartupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameManager>().To<GameManager>().AsSingle();
        }
    }
}