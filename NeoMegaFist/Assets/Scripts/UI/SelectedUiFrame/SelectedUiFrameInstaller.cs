using UnityEngine;
using Zenject;
using Ui.SelectedUiFrame;

public class SelectedUiFrameInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject selectedUiFrameControllerHaveObj;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SelectedUiFrameController>()
            .FromComponentOn(selectedUiFrameControllerHaveObj)
            .AsCached();
    }
}