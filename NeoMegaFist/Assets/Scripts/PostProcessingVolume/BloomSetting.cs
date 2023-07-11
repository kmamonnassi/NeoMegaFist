using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        /// �u���[���̖��邳��ݒ肷��
        /// </summary>
        /// <param name="intensity">���邳�ݒ�</param>
        public void SetBloomIntensity(float intensity)
        {
            bloom.intensity.value = intensity;
        }
    }
}
