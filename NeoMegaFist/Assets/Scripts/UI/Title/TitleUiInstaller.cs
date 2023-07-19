using UnityEngine;
using Zenject;
using Ui.Title;

public class TitleUiInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject titleUiViewHaveObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TitleUiPresenter>()
            .AsCached()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<TitleUiModel>()
            .AsCached();

        Container.BindInterfacesAndSelfTo<TitleUiView>()
            .FromComponentOn(titleUiViewHaveObj)
            .AsCached();
    }
}