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
        /// �}�X�^�[���ʂ�ݒ肷��
        /// </summary>
        /// <param name="volume">0�`1�܂ł̉���</param>
        public void SetMasterVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetMasterVolume(volume);
        }

        /// <summary>
        /// BGM���ʂ�ݒ肷��
        /// </summary>
        /// <param name="volume">0�`1�܂ł̉���</param>
        public void SetBgmVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetBgmVolume(volume);
        }

        /// <summary>
        /// SE���ʂ�ݒ肷��
        /// </summary>
        /// <param name="volume">0�`1�܂ł̉���</param>
        public void SetSeVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetSeVolume(volume);
        }
    }
}
