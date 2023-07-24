using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Audio;
using UnityEngine.EventSystems;
using InputControl;
using Zenject;

namespace Ui.VolumeSettingSliders
{
    public class VolumeSettingSlidersView : MonoBehaviour
    {
        [SerializeField]
        private Slider masterVolumeSlider;

        [SerializeField]
        private Slider bgmVolumeSlider;

        [SerializeField]
        private Slider seVolumeSlider;

        [SerializeField]
        private TextMeshProUGUI masterVolumeValueText;

        [SerializeField]
        private TextMeshProUGUI bgmVolumeValueText;

        [SerializeField]
        private TextMeshProUGUI seVolumeValueText;

        [Inject]
        private IInputer inputer;

        private ReactiveProperty<float> masterVolumeValue = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<float> masterVolumeValueProp => masterVolumeValue;

        private ReactiveProperty<float> bgmVolumeValue = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<float> bgmVolumeValueProp => bgmVolumeValue;

        private ReactiveProperty<float> seVolumeValue = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<float> seVolumeValueProp => seVolumeValue;

        private void Awake()
        {
            masterVolumeSlider.OnValueChangedAsObservable()
                .Subscribe(value => OnValueChanged(value, masterVolumeValue, masterVolumeValueText))
                .AddTo(gameObject);

            bgmVolumeSlider.OnValueChangedAsObservable()
                .Subscribe(value => OnValueChanged(value, bgmVolumeValue, bgmVolumeValueText))
                .AddTo(gameObject);

            seVolumeSlider.OnValueChangedAsObservable()
                .Subscribe(value => OnValueChanged(value, seVolumeValue, seVolumeValueText))
                .AddTo(gameObject);
        }

        private void Start()
        {
            if(inputer.GetControllerType() == ControllerType.Gamepad)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(masterVolumeSlider.gameObject);
            }
        }

        public void SetAllVolumeSliderValue(VolumeData volumeData)
        {
            masterVolumeSlider.value = volumeData.masterVolumeData;
            bgmVolumeSlider.value = volumeData.bgmVolumeData;
            seVolumeSlider.value = volumeData.seVolumeData;
        }

        /// <summary>
        /// スライダーに変更があった場合の動き
        /// </summary>
        /// <param name="volumeValue">音量</param>
        /// <param name="reactiveProperty">ReactiveProperty</param>
        private void OnValueChanged(float volumeValue, ReactiveProperty<float> reactiveProperty, TextMeshProUGUI textMeshPro)
        {
            reactiveProperty.Value = volumeValue;
            textMeshPro.text = Mathf.Floor(volumeValue * 100f).ToString();
        }
    }
}
