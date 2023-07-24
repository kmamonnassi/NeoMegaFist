using UnityEngine;
using UnityEngine.UI;
using UniRx;
using PostProcessingVolume;
using Zenject;
using UnityEngine.EventSystems;
using InputControl;

namespace Ui.DisplaySetting
{
    public class DisplaySettingView : MonoBehaviour
    {
        [SerializeField]
        private Toggle bloomToggle;

        [Inject]
        private IInputer inputer;

        private ReactiveProperty<bool> bloomEnableValue = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> bloomEnableValueProp => bloomEnableValue;

        private void Awake()
        {
            bloomToggle.OnValueChangedAsObservable()
                .Subscribe(value => bloomEnableValue.Value = value)
                .AddTo(gameObject);
        }

        private void Start()
        {
            if (inputer.GetControllerType() == ControllerType.Gamepad)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(bloomToggle.gameObject);
            }
        }

        /// <summary>
        /// BloomŠÖŒW‚Ì€–Ú‚ğ‚·‚×‚Äİ’è‚·‚é
        /// </summary>
        /// <param name="data">Bloomİ’èƒf[ƒ^</param>
        public void SetAllBloomSettingData(BloomSettingData data)
        {
            bloomEnableValue.Value = data.bloomEnable;
            bloomToggle.isOn = data.bloomEnable;
        }
    }

}
