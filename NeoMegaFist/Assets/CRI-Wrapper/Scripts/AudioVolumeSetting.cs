using UnityEngine;
using Zenject;
using CriWare;

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

        public AudioVolumeSetting()
        {
            VolumeData loadedData = LoadVolumeData();
            volumeData.masterVolumeData = loadedData.masterVolumeData;
            volumeData.seVolumeData = loadedData.seVolumeData;
            volumeData.bgmVolumeData = loadedData.bgmVolumeData;
        }

        // InjectÇégÇ¡ÇΩèâä˙âªÇÕÇ±Ç±Ç≈çsÇ§
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

            JsonUtilityExtensions.CheckJsonDirectory();
            JsonUtilityExtensions.WriteJson(volumeData, AudioSettingStaticData.VOLUME_SETTING_JSON_NAME);
        }

        private VolumeData LoadVolumeData()
        {
            VolumeData loadedVolumeData = JsonUtilityExtensions.ReadJson<VolumeData>(AudioSettingStaticData.VOLUME_SETTING_JSON_NAME);
            if (loadedVolumeData != null)
            {
                masterVolume = loadedVolumeData.masterVolumeData;
                seVolume = loadedVolumeData.seVolumeData;
                bgmVolume = loadedVolumeData.bgmVolumeData;
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
