using UnityEngine;
using Zenject;
using CriWare;
using System.IO;
using System;

// TODO:Jsonをいい感じに保存する便利なclassを作る
// TODO:Json保存でMakeJsonDataPathを使うとエラーになる
namespace Audio
{
    public class AudioVolumeSetting : IInitializable, IAudioVolumeSettable
    {
        [Inject]
        private IGettableAcfEnumInfo acfEnumInfo;

        private float masterVolume;
        public float masterVolumeProp => masterVolume;

        private float seVolume;
        public float seVolumeProp => seVolume;

        private float bgmVolume;
        public float bgmVolumeProp => bgmVolume;

        private VolumeData volumeData = new VolumeData();
        private GetSetAudioJsonData getSetAudioJsonData = new GetSetAudioJsonData();

        public AudioVolumeSetting()
        {
            VolumeData loadedData = LoadVolumeData();
            volumeData.masterVolumeData = loadedData.masterVolumeData;
            volumeData.seVolumeData = loadedData.seVolumeData;
            volumeData.bgmVolumeData = loadedData.bgmVolumeData;
        }

        // Injectを使った初期化はここで行う
        public void Initialize()
        {
            ((IAudioVolumeSettable)this).SetMasterVolume(masterVolume);
            ((IAudioVolumeSettable)this).SetSeVolume(seVolume);
            ((IAudioVolumeSettable)this).SetBgmVolume(bgmVolume);

            ((IAudioVolumeSettable)this).SaveVolumeData();
        }

        void IAudioVolumeSettable.SetMasterVolume(float ratio)
        {
            masterVolume = Mathf.Clamp01(ratio);
            volumeData.masterVolumeData = masterVolume;
            CriAtom.SetCategoryVolume(acfEnumInfo.bgmCategoryNameProp, bgmVolume * masterVolume);
            CriAtom.SetCategoryVolume(acfEnumInfo.seCategoryNameProp, seVolume * masterVolume);
        }

        void IAudioVolumeSettable.SetSeVolume(float ratio)
        {
            seVolume = Mathf.Clamp01(ratio);
            volumeData.seVolumeData = seVolume;
            CriAtom.SetCategoryVolume(acfEnumInfo.seCategoryNameProp, seVolume * masterVolume);
        }

        void IAudioVolumeSettable.SetBgmVolume(float ratio)
        {
            bgmVolume = Mathf.Clamp01(ratio);
            volumeData.bgmVolumeData = bgmVolume;
            CriAtom.SetCategoryVolume(acfEnumInfo.bgmCategoryNameProp, bgmVolume * masterVolume);
        }

        void IAudioVolumeSettable.SaveVolumeData()
        {
            volumeData.masterVolumeData = masterVolume;
            volumeData.seVolumeData = seVolume;
            volumeData.bgmVolumeData = bgmVolume;

            getSetAudioJsonData.SaveVolumeData(volumeData);
        }

        private VolumeData LoadVolumeData()
        {
            VolumeData loadedVolumeData = getSetAudioJsonData.LoadVolumeData();
            if (loadedVolumeData != null)
            {
                return loadedVolumeData;
            }
            else
            {
                masterVolume = AudioSettingStaticData.START_VOLUME_MASTER;
                seVolume = AudioSettingStaticData.START_VOLUME_SE;
                bgmVolume = AudioSettingStaticData.START_VOLUME_BGM;

                loadedVolumeData = new VolumeData();
                loadedVolumeData.masterVolumeData = masterVolume;
                loadedVolumeData.seVolumeData = seVolume;
                loadedVolumeData.bgmVolumeData = bgmVolume;
                return loadedVolumeData;
            }
        }

        VolumeData IAudioVolumeSettable.GetVolumeData()
        {
            return volumeData;
        }
    }
}
