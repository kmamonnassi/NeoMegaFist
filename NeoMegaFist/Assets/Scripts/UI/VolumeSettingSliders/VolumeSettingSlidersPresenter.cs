using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace UI.VolumeSettingSliders
{
    public class VolumeSettingSlidersPresenter : MonoBehaviour
    {
        [SerializeField]
        private VolumeSettingSlidersView view;

        [SerializeField]
        private VolumeSettingSlidersModel model;

        private void Awake()
        {
            view.masterVolumeValueProp.Skip(1)
                .Subscribe(value => { model.SetMasterVolume(value); })
                .AddTo(gameObject);

            view.bgmVolumeValueProp.Skip(1)
                .Subscribe(value => { model.SetBgmVolume(value); })
                .AddTo(gameObject);

            view.seVolumeValueProp.Skip(1)
                .Subscribe(value => { model.SetSeVolume(value); })
                .AddTo(gameObject);
        }
    }
}
