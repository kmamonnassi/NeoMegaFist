using UnityEngine;
using Zenject;

namespace StageObject
{
    public class OverhandThrownImpactInstaller : MonoInstaller
    {
        [SerializeField] private OverhandThrownImpact impactPrefab;
        public override void InstallBindings()
        {
            Container.Bind<OverhandThrownImpact>().FromInstance(impactPrefab);
        }
    }
}
