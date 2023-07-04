using UnityEngine;
using Zenject;
using Ui.InputGuardCanvas;

public class UiInputGuardCanvasInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject uiInputGuardCanvasObj;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UiInputGuardCanvas>()
            .FromComponentOn(uiInputGuardCanvasObj)
            .AsCached();
    }
}