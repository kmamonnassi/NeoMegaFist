using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using PostProcessingVolume;

namespace Ui.DisplaySetting
{
    public class DisplaySettingModel
    {
        [Inject]
        private BloomSetting bloomSetting;

        [Inject]
        private IPostProcessingVolumeSavable ppsVolumeSave;

        public BloomSettingData GetBloomSettingData()
        {
            return bloomSetting.GetBloomSettingData();
        }

        public void SetBloomEnabe(bool enable)
        {
            bloomSetting.SetBloomEnable(enable);
        }

        public void SavePpsData()
        {
            ppsVolumeSave.SavePostProcessingVolumeData();
        }
    }
}
