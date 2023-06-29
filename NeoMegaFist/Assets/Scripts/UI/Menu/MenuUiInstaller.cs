using UnityEngine;
using Zenject;

public class MenuUiInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject optionTabGroupObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<OptionTabGroup>()
            .FromComponentOn(optionTabGroupObj)
            .AsCached()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<UI.VolumeSettingSliders.VolumeSettingSlidersModel>()
            .AsCached();
    }
}