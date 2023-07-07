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
    void Start()
    {
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AudioReserveManager.AudioReserve("Hoge", "AAA", transform);
        }

        if (Input.GetMouseButtonDown(1))
        {
            AudioReserveManager.AudioReserve("Hoge", "BBB", transform);
        }

        if (Input.GetMouseButtonDown(2))
        {
            AudioReserveManager.AudioReserve("Huga", "CCC", transform);
        }

    }
}
