using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PostProcessingVolume
{
    public struct BloomSettingData
    {
        public bool bloomEnable;

        public BloomSettingData(bool bloomEnable)
        {
            this.bloomEnable = bloomEnable;
        }
    }

    public class BloomSetting
    {
        private Bloom bloom;

        public BloomSetting(Volume volume)
        {
            volume.profile.TryGet(out bloom);
        }

        /// <summary>
        /// Bloomの設定データを取得する
        /// </summary>
        public BloomSettingData GetBloomSettingData()
        {
            return new BloomSettingData(
                GetBloomEnable());
        }

        /// <summary>
        /// ブルームの有効無効を切り替える
        /// </summary>
        /// <param name="enable">有効無効</param>
        public void SetBloomEnable(bool enable)
        {
            bloom.active = enable;
        }

        /// <summary>
        /// Bloomの有効無効を取得する
        /// </summary>
        public bool GetBloomEnable()
        {
            return bloom.active;
        }
    }
}
