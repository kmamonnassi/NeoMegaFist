using UnityEngine;
using Zenject;

namespace Stage
{
    public class TilemapInstaller : MonoInstaller
    {
        [SerializeField] private TilemapGetter tilemapGetter;

        public override void InstallBindings()
        {
            Container.Bind<ITilemapGetter>().FromInstance(tilemapGetter);
        }
    }
}
