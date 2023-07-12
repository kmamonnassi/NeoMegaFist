using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PostProcessingVolume
{
    public class BloomSetting
    {
        private Bloom bloom;

        public BloomSetting(Volume volume)
        {
            volume.profile.TryGet(out bloom);
        }

        /// <summary>
        /// �u���[���̋��x��ݒ肷��
        /// </summary>
        /// <param name="intensity">���邳�ݒ�</param>
        public void SetBloomIntensity(float intensity)
        {
            bloom.intensity.value = intensity;
        }

        /// <summary>
        /// �u���[���̋��x���擾����
        /// </summary>
        public float GetBloomIntensity()
        {
            return bloom.intensity.value;
        }
    }
}
