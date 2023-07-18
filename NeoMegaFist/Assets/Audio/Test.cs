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
    private ISePlayable sePlayer;

    void Start()
    {

    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            sePlayer.Play("Dadabo", "Dadabo_AttackPreparation", gameObject.GetInstanceID());
        }

        if (Input.GetMouseButtonDown(1))
        {
            sePlayer.Play("Dadabo", "Dadabo_Chase", gameObject.GetInstanceID());
        }
    }
}
