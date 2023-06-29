using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.VolumeSettingSliders;
using Zenject;

public class AudioTab : OptionTab
{
    //[Inject]
    private VolumeSettingSlidersModel model;

    [Inject]
    DiContainer container;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //Debug.Log(model);
    }
}
