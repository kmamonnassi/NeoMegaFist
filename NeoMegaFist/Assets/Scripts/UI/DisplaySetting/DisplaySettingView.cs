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
