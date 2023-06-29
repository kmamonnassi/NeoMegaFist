using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using Zenject;
using UnityEngine.InputSystem;
using UniRx;
using UI.DisplayInteract;

public class Test : MonoBehaviour
{
    [Inject]
    private ISePlayable sePlayable;

    //[Inject]
    //private IDisplayableInteractModel model;

    [SerializeField]
    private UI.DisplayInteract.DisplayInteractSpriteAsset spriteAsset;


    void Start()
    {
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("AAA");
            AudioReserveManager.AudioReserve("Hoge", "Fuga", transform);
        }
    }
}
