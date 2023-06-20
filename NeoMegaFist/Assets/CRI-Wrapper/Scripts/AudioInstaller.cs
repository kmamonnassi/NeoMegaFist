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

        // TODO:����Ԉ���ĂȂ����H
        Container.BindInterfacesAndSelfTo<AudioReserveManager>()
                 .FromComponentOn(audioReserveObj)
                 .AsSingle();
    }
}