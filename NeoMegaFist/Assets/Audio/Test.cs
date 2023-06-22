using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject]
    private ISePlayable sePlayable;

    void Start()
    {
        AudioReserveManager.AudioReserve("Player", "çUåÇ", transform);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AudioReserveManager.AudioReserve("Player", "çUåÇ", transform);
        }
    }
}
