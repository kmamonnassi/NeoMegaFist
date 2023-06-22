using UnityEngine;
using Zenject;
using CriWare;
using System.IO;

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

        private VolumeData volumeData = null;

        public AudioVolumeSetting()
        {
            volumeData = LoadVolumeData();
            if (volumeData == null)
            {
                masterVolume = AudioSettingStaticData.START_VOLUME_MASTER;
                seVolume = AudioSettingStaticData.START_VOLUME_SE;
                bgmVolume = AudioSettingStaticData.START_VOLUME_BGM;
            }
            else
            {
                masterVolume = volumeData.masterVolumeData;
                seVolume = volumeData.seVolumeData;
                bgmVolume = volumeData.bgmVolumeData;
            }
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
            CriAtom.SetCategoryVolume(acfEnumInfo.bgmCategoryNameProp, bgmVolume * masterVolume);
            CriAtom.SetCategoryVolume(acfEnumInfo.seCategoryNameProp, seVolume * masterVolume);
        }

        void IAudioVolumeSettable.SetSeVolume(float ratio)
        {
            seVolume = Mathf.Clamp01(ratio);
            CriAtom.SetCategoryVolume(acfEnumInfo.seCategoryNameProp, seVolume * masterVolume);
        }

        void IAudioVolumeSettable.SetBgmVolume(float ratio)
        {
            bgmVolume = Mathf.Clamp01(ratio);
            CriAtom.SetCategoryVolume(acfEnumInfo.bgmCategoryNameProp, bgmVolume * masterVolume);
        }

        void IAudioVolumeSettable.SaveVolumeData()
        {
            VolumeData volumeData = new VolumeData();
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

            // データが無ければnull
            if (string.IsNullOrEmpty(dataStr))
            {
                streamReader.Close();
                return null;
            }

            VolumeData volumeData = JsonUtility.FromJson<VolumeData>(dataStr);
            return volumeData;
        }
    }
}
