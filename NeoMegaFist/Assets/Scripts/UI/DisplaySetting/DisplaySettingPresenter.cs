using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

namespace Ui.DisplaySetting
{
    public class DisplaySettingPresenter : IInitializable
    {
        [Inject]
        private DisplaySettingView view;

        [Inject]
        private DisplaySettingModel model;

        public void Initialize()
        {
            view.bloomEnableValueProp.Skip(1)
                .Subscribe(value => model.SetBloomEnabe(value));
        }
    }
}
