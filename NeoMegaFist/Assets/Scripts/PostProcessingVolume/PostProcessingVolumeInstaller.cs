using UnityEngine;
using Zenject;
using PostProcessingVolume;

public class PostProcessingVolumeInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject postProcessingVolumeObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PostProcessingVolumeSetting>()
            .FromComponentOn(postProcessingVolumeObj)
            .AsSingle();
    }
}