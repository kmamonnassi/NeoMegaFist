using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using Zenject;
using UniRx;
using System;

namespace UI.VolumeSettingSliders
{
    public class VolumeSettingSlidersModel : IInitializable
    {
        [Inject]
        private IAudioVolumeSettable volumeSettable;

        public Subject<VolumeData> volumeSetHandler = new Subject<VolumeData>();

        void IInitializable.Initialize()
        {
            InitVolumeSlider();
        }

        /// <summary>
        /// 音量スライダーを初期化
        /// </summary>
        private void InitVolumeSlider()
        {
            volumeSetHandler.OnNext(GetVolumeData());
        }

        /// <summary>
        /// 音声データを取得する
        /// </summary>
        public VolumeData GetVolumeData()
        {
            return volumeSettable.GetVolumeData();
        }

        /// <summary>
        /// マスター音量を設定する
        /// </summary>
        /// <param name="volume">0～1までの音量</param>
        public void SetMasterVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetMasterVolume(volume);
        }

        /// <summary>
        /// BGM音量を設定する
        /// </summary>
        /// <param name="volume">0～1までの音量</param>
        public void SetBgmVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetBgmVolume(volume);
        }

        /// <summary>
        /// SE音量を設定する
        /// </summary>
        /// <param name="volume">0～1までの音量</param>
        public void SetSeVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            volumeSettable.SetSeVolume(volume);
        }

    }
}
