using UnityEngine;
using UnityEngine.UI;
using UniRx;
using PostProcessingVolume;

namespace Ui.DisplaySetting
{
    public class DisplaySettingView : MonoBehaviour
    {
        [SerializeField]
        private Toggle bloomToggle;

        private ReactiveProperty<bool> bloomEnableValue = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> bloomEnableValueProp => bloomEnableValue;

        private void Awake()
        {
            bloomToggle.OnValueChangedAsObservable()
                .Subscribe(value => bloomEnableValue.Value = value)
                .AddTo(gameObject);
        }

        /// <summary>
        /// Bloom�֌W�̍��ڂ����ׂĐݒ肷��
        /// </summary>
        /// <param name="data">Bloom�ݒ�f�[�^</param>
        public void SetAllBloomSettingData(BloomSettingData data)
        {
            bloomEnableValue.Value = data.bloomEnable;
            bloomToggle.isOn = data.bloomEnable;
        }
    }

}
