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
        /// マスター音量を設定する
        /// </summary>
        /// <param name="volume">0〜1までの音量</param>
        public void SetMasterVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetMasterVolume(volume);
        }

        /// <summary>
        /// BGM音量を設定する
        /// </summary>
        /// <param name="volume">0〜1までの音量</param>
        public void SetBgmVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetBgmVolume(volume);
        }

        /// <summary>
        /// SE音量を設定する
        /// </summary>
        /// <param name="volume">0〜1までの音量</param>
        public void SetSeVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetSeVolume(volume);
        }
    }
}
