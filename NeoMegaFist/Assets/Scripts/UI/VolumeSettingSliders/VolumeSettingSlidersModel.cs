using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using Zenject;

namespace UI.VolumeSettingSliders
{
    public class VolumeSettingSlidersModel : MonoBehaviour
    {
        [Inject]
        private IAudioVolumeSettable volumeSettable;

        /// <summary>
        /// É}ÉXÉ^Å[âπó Çê›íËÇ∑ÇÈ
        /// </summary>
        /// <param name="volume">0Å`1Ç‹Ç≈ÇÃâπó </param>
        public void SetMasterVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetMasterVolume(volume);
        }

        /// <summary>
        /// BGMâπó Çê›íËÇ∑ÇÈ
        /// </summary>
        /// <param name="volume">0Å`1Ç‹Ç≈ÇÃâπó </param>
        public void SetBgmVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetBgmVolume(volume);
        }

        /// <summary>
        /// SEâπó Çê›íËÇ∑ÇÈ
        /// </summary>
        /// <param name="volume">0Å`1Ç‹Ç≈ÇÃâπó </param>
        public void SetSeVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetSeVolume(volume);
        }
    }
}
