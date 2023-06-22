using UnityEngine;
using Zenject;

namespace Utility.PostEffect
{
    public class PostEffectInstaller : MonoInstaller
    {
        [SerializeField] private PostEffectCamera postEffectCamera;
        [SerializeField] private PostEffectMaterialDB postEffectMaterialDB;

        public override void InstallBindings()
        {
            Container.BindInstance<IPostEffectCamera>(postEffectCamera);
            Container.BindInstance<IPostEffectMaterialDB>(postEffectMaterialDB);
            Container.Bind<IPostEffector>().To<PostEffector>().AsSingle();
        }
    }
}