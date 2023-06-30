using UnityEngine;
using Zenject;
using Ui.VolumeSettingSliders;
using Ui.Menu;

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

        Container.BindInterfacesAndSelfTo<VolumeSettingSlidersModel>().AsCached();
    }
}