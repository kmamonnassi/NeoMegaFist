using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace PostProcessingVolume
{
    public class PostProcessingVolumeSetting : MonoBehaviour, IPostProcessingVolumeSavable
    {
        [SerializeField]
        private Volume volume;

        [Inject]
        DiContainer diContainer;


        private PostProcessingVolumeData volumeData;

        private BloomSetting bloomSetting;

        private void Awake()
        {
            BindSettingClass();

            volumeData = LoadVolumeData();
            bloomSetting.SetBloomIntensity(volumeData.bloomIntensity);
            // TODO:�Z�[�u�f�[�^���烍�[�h����
            // TODO:Editor�Ȃ�SerializeField���玝���Ă�����
        }

        /// <summary>
        /// �e���ڂ�ݒ肷��N���X��Bind����
        /// </summary>
        private void BindSettingClass()
        {
            bloomSetting = new BloomSetting(volume);
            diContainer.BindInstance(bloomSetting);
        }

        /// <summary>
        /// PPS�̃p�����[�^�[�����[�h����
        /// </summary>
        private PostProcessingVolumeData LoadVolumeData()
        {
            PostProcessingVolumeData loadedVolumeData = JsonUtilityExtensions.ReadJson<PostProcessingVolumeData>(PostProcessingVolumeSettingStaticData.PPS_SETTING_JSON_NAME);
            if(loadedVolumeData != null)
            {
                return loadedVolumeData;
            }
            else
            {
                loadedVolumeData = new PostProcessingVolumeData();
                loadedVolumeData.bloomIntensity = PostProcessingVolumeSettingStaticData.START_BLOOM_INTENSITY;
                return loadedVolumeData;
            }
        }

        void IPostProcessingVolumeSavable.SavePostProcessingVolumeData()
        {
            PostProcessingVolumeData volumeData = new PostProcessingVolumeData();
            volumeData.bloomIntensity = bloomSetting.GetBloomIntensity();

            JsonUtilityExtensions.CheckJsonDirectory();
            JsonUtilityExtensions.WriteJson(volumeData, PostProcessingVolumeSettingStaticData.PPS_SETTING_JSON_NAME);
        }
    }
}
