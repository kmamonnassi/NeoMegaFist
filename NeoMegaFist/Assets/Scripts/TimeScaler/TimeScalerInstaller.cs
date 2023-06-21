using UnityEngine;
using Zenject;

public class TimeScalerInstaller : MonoInstaller
{
    [SerializeField] private TimeScaler timeScaler;

    public override void InstallBindings()
    {
        Container.Bind<ITimeScaler>().FromInstance(timeScaler).AsSingle();
    }
}
