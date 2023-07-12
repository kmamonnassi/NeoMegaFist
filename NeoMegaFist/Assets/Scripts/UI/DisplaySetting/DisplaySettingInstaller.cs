using UnityEngine;
using Zenject;
using Ui.DisplaySetting;

public class DisplaySettingInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject displayViewHasObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<DisplaySettingPresenter>()
            .AsCached()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<DisplaySettingModel>()
            .AsCached();

        Container.BindInterfacesAndSelfTo<DisplaySettingView>()
            .FromComponentOn(displayViewHasObj)
            .AsCached();
    }
}