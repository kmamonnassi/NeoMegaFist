using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using Zenject;
using UnityEngine.InputSystem;
using UniRx;
using UI.DisplayInteract;
using InputControl;

public class Test : MonoBehaviour
{
    [Inject]
    private ISePlayable sePlayable;

    //[Inject]
    //private IDisplayableInteractModel model;

    [Inject]
    private IInputer inputer;

    [SerializeField]
    private UI.DisplayInteract.DisplayInteractSpriteAsset spriteAsset;


    void Start()
    {
        Debug.Log(inputer.GetControllerType());
    }


    void Update()
    {

       
    }
}
