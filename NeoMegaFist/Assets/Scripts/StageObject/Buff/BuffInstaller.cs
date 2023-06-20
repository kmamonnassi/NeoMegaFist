using Zenject;

namespace StageObject.Buff
{
    public class BuffInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBuffDB>().To<BuffDB>().AsSingle();
        }
    }
}