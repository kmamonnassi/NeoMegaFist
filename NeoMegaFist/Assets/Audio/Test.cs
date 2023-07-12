using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using Zenject;
using UnityEngine.InputSystem;
using UniRx;
using Ui.DisplayInteract;
using InputControl;

public class Test : MonoBehaviour
{
    [Inject]
    private PostProcessingVolume.BloomSetting bloomSetting;

    [Inject]
    private PostProcessingVolume.IPostProcessingVolumeSavable savable;

    void Start()
    {
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            bloomSetting.SetBloomIntensity(0f);
        }

        if (Input.GetMouseButtonDown(1))
        {
            savable.SavePostProcessingVolumeData();
        }

    }
}
