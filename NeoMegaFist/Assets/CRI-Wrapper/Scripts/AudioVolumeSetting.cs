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

            //StreamWriter streamWriter;
            string jsonStr = JsonUtility.ToJson(volumeData, true);
#if UNITY_EDITOR
            if (!Directory.Exists(Application.dataPath + AudioSettingStaticData.JSON_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(Application.dataPath + AudioSettingStaticData.JSON_DIRECTORY_PATH);
            }

            //if (!File.Exists(Application.dataPath + AudioSettingStaticData.VOLUME_SETTING_PATH))
            //{
            //    File.Create(Application.dataPath + AudioSettingStaticData.VOLUME_SETTING_PATH);
            //}
            //streamWriter = new StreamWriter(Application.dataPath + AudioSettingStaticData.VOLUME_SETTING_PATH, false);
            string dataPath = Application.dataPath + AudioSettingStaticData.VOLUME_SETTING_PATH;
            File.WriteAllText(dataPath, jsonStr);
#else
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.JSON_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.JSON_DIRECTORY_PATH);
            }
            string dataPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.VOLUME_SETTING_PATH;
            File.WriteAllText(dataPath, jsonStr);
#endif
        }

        private VolumeData LoadVolumeData()
        {
            string dataStr = string.Empty;
            StreamReader streamReader = default;
#if UNITY_EDITOR
            if (File.Exists(Application.dataPath + AudioSettingStaticData.VOLUME_SETTING_PATH))
            {
                streamReader = new StreamReader(Application.dataPath + AudioSettingStaticData.VOLUME_SETTING_PATH);
            }
#else
        if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.JSON_DIRECTORY_PATH))
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.JSON_DIRECTORY_PATH);
        }
        
        if(File.Exists(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.VOLUME_SETTING_PATH))
        {
            streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.VOLUME_SETTING_PATH);
        }
#endif
            if (streamReader != null)
            {
                dataStr = streamReader.ReadToEnd();
                streamReader.Close();
            }

            if (string.IsNullOrEmpty(dataStr))
            {
                streamReader?.Close();

                masterVolume = AudioSettingStaticData.START_VOLUME_MASTER;
                seVolume = AudioSettingStaticData.START_VOLUME_SE;
                bgmVolume = AudioSettingStaticData.START_VOLUME_BGM;

                VolumeData unloadedData = new VolumeData();
                unloadedData.masterVolumeData = masterVolume;
                unloadedData.seVolumeData = seVolume;
                unloadedData.bgmVolumeData = bgmVolume;
                return unloadedData;
            }

            VolumeData data = JsonUtility.FromJson<VolumeData>(dataStr);

            masterVolume = data.masterVolumeData;
            seVolume = data.seVolumeData;
            bgmVolume = data.bgmVolumeData;

            return data;
        }

        VolumeData IAudioVolumeSettable.GetVolumeData()
        {
            return volumeData;
        }
    }
}
