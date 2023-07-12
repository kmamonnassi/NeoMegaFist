using UnityEngine;
using UnityEngine.Rendering;
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
            bloomSetting.SetBloomEnable(volumeData.bloomEnable);
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
            PostProcessingVolumeData loadedVolumeData;

#if UNITY_EDITOR
            loadedVolumeData = GetVolumeDataFromVolumeComponent();
            return loadedVolumeData;
#else

            loadedVolumeData = JsonUtilityExtensions.ReadJson<PostProcessingVolumeData>(PostProcessingVolumeSettingStaticData.PPS_SETTING_JSON_NAME);
            if(loadedVolumeData != null)
            {
                return loadedVolumeData;
            }
            else
            {
                loadedVolumeData = new PostProcessingVolumeData();
                loadedVolumeData.bloomEnable = PostProcessingVolumeSettingStaticData.START_BLOOM_ENABLE;
                return loadedVolumeData;
            }
#endif
        }

        /// <summary>
        /// Volume�R���|�[�l���g����f�[�^���擾����
        /// </summary>
        private PostProcessingVolumeData GetVolumeDataFromVolumeComponent()
        {
            PostProcessingVolumeData data = new PostProcessingVolumeData();
            data.bloomEnable = bloomSetting.GetBloomEnable();

            return data;
        }

        void IPostProcessingVolumeSavable.SavePostProcessingVolumeData()
        {
            PostProcessingVolumeData volumeData = GetVolumeDataFromVolumeComponent();

            JsonUtilityExtensions.CheckJsonDirectory();
            JsonUtilityExtensions.WriteJson(volumeData, PostProcessingVolumeSettingStaticData.PPS_SETTING_JSON_NAME);
        }
    }
}
