using UnityEngine;
using Zenject;
using Ui.Modal;

public class ModalUiContainerInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject modalUiContainerObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ModalUiContainer>()
            .FromComponentOn(modalUiContainerObj)
            .AsSingle();
    }
}