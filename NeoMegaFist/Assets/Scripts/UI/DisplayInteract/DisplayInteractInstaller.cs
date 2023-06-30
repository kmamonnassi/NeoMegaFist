using UnityEngine;
using Zenject;
using Ui.DisplayInteract;

public class DisplayInteractInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject displayInteractCanvasObj;

    [SerializeField]
    private GameObject displayInteractRootObj;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<DisplayInteractModel>()
            .FromComponentOn(displayInteractRootObj)
            .AsCached();

        Container.BindInterfacesAndSelfTo<DisplayInteractCanvasView>()
            .FromComponentOn(displayInteractCanvasObj)
            .AsCached();

        Container.BindInterfacesAndSelfTo<DisplayInteractPresenter>()
            .AsCached()
            .NonLazy();
    }
}