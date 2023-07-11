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
        /// ブルームの明るさを設定する
        /// </summary>
        /// <param name="intensity">明るさ設定</param>
        public void SetBloomIntensity(float intensity)
        {
            bloom.intensity.value = intensity;
        }
    }
}
