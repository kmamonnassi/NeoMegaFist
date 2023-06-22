using Zenject;

namespace InputControl
{
    public class InputerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputer>().To<Inputer>().AsSingle();
        }
    }
}
