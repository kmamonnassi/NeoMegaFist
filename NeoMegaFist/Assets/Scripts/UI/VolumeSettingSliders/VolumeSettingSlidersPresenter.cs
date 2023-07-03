using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using System;

namespace Ui.VolumeSettingSliders
{
    public class VolumeSettingSlidersPresenter : IInitializable
    {
        [Inject]
        private VolumeSettingSlidersView view;

        [Inject]
        private VolumeSettingSlidersModel model;

        public void Initialize()
        {
            view.masterVolumeValueProp.Skip(1)
                .Subscribe(value => { model.SetMasterVolume(value); });

            view.bgmVolumeValueProp.Skip(1)
                .Subscribe(value => { model.SetBgmVolume(value); });

            view.seVolumeValueProp.Skip(1)
                .Subscribe(value => { model.SetSeVolume(value); });

            model.volumeSetHandler.Subscribe(value => view.SetAllVolumeSliderValue(value));
        }
    }
}
