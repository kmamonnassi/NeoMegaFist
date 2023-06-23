using UnityEngine;
using Zenject;
using UI.VolumeSettingSliders;

public class VolumeSettingSlidersInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject slidersViewHaveObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<VolumeSettingSlidersPresenter>()
            .AsCached()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<VolumeSettingSlidersModel>()
            .AsCached();

        Container.BindInterfacesAndSelfTo<VolumeSettingSlidersView>()
            .FromComponentOn(slidersViewHaveObj)
            .AsCached();
    }
}