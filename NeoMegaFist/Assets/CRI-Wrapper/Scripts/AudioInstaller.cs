using UnityEngine;
using Zenject;
using Audio;

public class AudioInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject audioReserveObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AcfEnumInfo>()
                 .AsSingle();

        Container.BindInterfacesAndSelfTo<AudioManager>()
                 .AsSingle();

        Container.BindInterfacesAndSelfTo<AudioVolumeSetting>()
                 .AsSingle();

        // TODO:Ç±ÇÍä‘à·Ç¡ÇƒÇ»Ç¢Ç©ÅH
        Container.BindInterfacesAndSelfTo<AudioReserveManager>()
                 .FromComponentOn(audioReserveObj)
                 .AsSingle();
    }
}