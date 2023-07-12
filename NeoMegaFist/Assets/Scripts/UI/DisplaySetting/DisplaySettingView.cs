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
        /// Bloom関係の項目をすべて設定する
        /// </summary>
        /// <param name="data">Bloom設定データ</param>
        public void SetAllBloomSettingData(BloomSettingData data)
        {
            bloomEnableValue.Value = data.bloomEnable;
            bloomToggle.isOn = data.bloomEnable;
        }
    }

}
