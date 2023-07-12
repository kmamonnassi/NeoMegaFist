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
        /// Bloom�̐ݒ�f�[�^���擾����
        /// </summary>
        public BloomSettingData GetBloomSettingData()
        {
            return new BloomSettingData(
                GetBloomEnable());
        }

        /// <summary>
        /// �u���[���̗L��������؂�ւ���
        /// </summary>
        /// <param name="enable">�L������</param>
        public void SetBloomEnable(bool enable)
        {
            bloom.active = enable;
        }

        /// <summary>
        /// Bloom�̗L���������擾����
        /// </summary>
        public bool GetBloomEnable()
        {
            return bloom.active;
        }
    }
}
