using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using InputControl;
using UnityEngine.EventSystems;

namespace Ui.OptionSetting
{
    public class OptionSettingView : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private GameEnd gameEnd;

        [Inject]
        private IInputer inputer;

        private void Awake()
        {
            button.OnClickAsObservable()
                .Subscribe(_ => gameEnd.GameEndConfirm())
                .AddTo(gameObject);
        }

        private void Start()
        {
            if (inputer.GetControllerType() == ControllerType.Gamepad)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }
        }
    }
}
