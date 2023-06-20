using UnityEngine;
using Zenject;

namespace Utility
{
    public class UpdaterInstaller : MonoInstaller
    {
        [SerializeField] Updater updater;

        public override void InstallBindings()
        {
            Container.Bind<IUpdater>().FromInstance(updater);
        }
    }
}