using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UniRx.Toolkit;
using Zenject;

public class TabSelectButton : MonoBehaviour
{
    [SerializeField]
    private OptionKinds optionKind;

    [SerializeField]
    private Button button;

    [SerializeField]
    private OptionTabGroup tabGroup;

    [Inject]
    private UI.VolumeSettingSliders.VolumeSettingSlidersModel model;

    void Start()
    {
        button.OnClickAsObservable()
            .Subscribe(_ => tabGroup.ShowTab(optionKind))
            .AddTo(this.gameObject);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(model.GetVolumeData().bgmVolumeData);
        }
    }
}
