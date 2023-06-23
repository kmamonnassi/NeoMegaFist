using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Audio
{
    public class GetSetAudioJsonData
    {
        /// <summary>
        /// 音量設定を保存する
        /// </summary>
        /// <param name="volumeData">音量データ</param>
        public void SaveVolumeData(VolumeData volumeData)
        {
            string jsonStr = JsonUtility.ToJson(volumeData, true);
            CheckDirectory();
            WriteJsonFile(jsonStr);
        }

        /// <summary>
        /// 音量データをロードする
        /// </summary>
        public VolumeData LoadVolumeData()
        {
            return ReadJsonFile();
        }

        private void CheckDirectory()
        {
            string path = string.Empty;

#if UNITY_EDITOR
            path = Application.dataPath + AudioSettingStaticData.JSON_DIRECTORY_PATH;
#else
            path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.JSON_DIRECTORY_PATH;
#endif

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void WriteJsonFile(string jsonStr)
        {
            string path = string.Empty;

#if UNITY_EDITOR
            path = Application.dataPath + AudioSettingStaticData.VOLUME_SETTING_PATH;
#else
            path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.VOLUME_SETTING_PATH;
#endif

            File.WriteAllText(path, jsonStr);
        }

        private VolumeData ReadJsonFile()
        {
            string path = string.Empty;
            string dataStr = string.Empty;
            VolumeData volumeData = new VolumeData();

#if UNITY_EDITOR
            path = Application.dataPath + AudioSettingStaticData.VOLUME_SETTING_PATH;
#else
            path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + AudioSettingStaticData.VOLUME_SETTING_PATH;
#endif

            if(File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path);
                dataStr = streamReader.ReadToEnd();
                streamReader.Close();

                volumeData = JsonUtility.FromJson<VolumeData>(dataStr);
                return volumeData;
            }
            else
            {
                return null;
            }
        }
    }

}
