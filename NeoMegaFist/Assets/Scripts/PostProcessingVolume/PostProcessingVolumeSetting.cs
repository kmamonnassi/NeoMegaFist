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
        }

        /// <summary>
        /// 各項目を設定するクラスをBindする
        /// </summary>
        private void BindSettingClass()
        {
            bloomSetting = new BloomSetting(volume);
            diContainer.BindInstance(bloomSetting);
        }

        /// <summary>
        /// PPSのパラメーターをロードする
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
                loadedVolumeData.bloomIntensity = PostProcessingVolumeSettingStaticData.START_BLOOM_INTENSITY;
                return loadedVolumeData;
            }
#endif
        }

        /// <summary>
        /// Volumeコンポーネントからデータを取得する
        /// </summary>
        private PostProcessingVolumeData GetVolumeDataFromVolumeComponent()
        {
            PostProcessingVolumeData data = new PostProcessingVolumeData();
            data.bloomIntensity = bloomSetting.GetBloomIntensity();

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
