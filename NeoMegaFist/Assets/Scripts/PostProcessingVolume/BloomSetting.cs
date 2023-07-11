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
        /// ƒuƒ‹[ƒ€‚Ì–¾‚é‚³‚ğİ’è‚·‚é
        /// </summary>
        /// <param name="intensity">–¾‚é‚³İ’è</param>
        public void SetBloomIntensity(float intensity)
        {
            bloom.intensity.value = intensity;
        }
    }
}
