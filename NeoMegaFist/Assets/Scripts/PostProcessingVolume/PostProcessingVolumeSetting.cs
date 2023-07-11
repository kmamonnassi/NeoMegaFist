using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace PostProcessingVolume
{
    public class PostProcessingVolumeSetting : MonoBehaviour
    {
        [SerializeField]
        private Volume volume;

        [Inject]
        DiContainer diContainer;

        private BloomSetting bloomSetting;

        private void Awake()
        {
            bloomSetting = new BloomSetting(volume);
            diContainer.BindInstance(bloomSetting);


            // TODO:セーブデータからロードする
        }

        
    }
}
