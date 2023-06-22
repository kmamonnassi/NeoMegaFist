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

        // TODO:これ間違ってないか？
        Container.BindInterfacesAndSelfTo<AudioReserveManager>()
                 .FromComponentOn(audioReserveObj)
                 .AsSingle();
    }
}