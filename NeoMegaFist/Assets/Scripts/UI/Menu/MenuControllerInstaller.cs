using UnityEngine;
using Zenject;
using Ui.Menu;

public class MenuControllerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BeforeSelectedOptionKind>()
            .AsCached();
    }
}