using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ui.VolumeSettingSliders;
using Zenject;

namespace Ui.Menu
{
    public class AudioTab : SettingTabBase
    {
        [Inject]
        private IVolumeSettable volume;

        public override void SaveSettingData()
        {
            if(!isSaved)
            {
                volume.SaveVolumeData();
                isSaved = true;
            }
        }
    }
}
