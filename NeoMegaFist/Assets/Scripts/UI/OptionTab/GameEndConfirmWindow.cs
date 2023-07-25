using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ui.Modal;
using Zenject;
using InputControl;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace Ui.Option
{
    // TODO:âºçÏê¨ÇÃÉNÉâÉX
    public class GameEndConfirmWindow : MonoBehaviour
    {
        [Inject]
        private IModalHistoryControllable modalHistoryController;

        [Inject]
        private IInputer inputer;

        [SerializeField]
        private Button noButton;

        void Start()
        {
            if (inputer.GetControllerType() == ControllerType.Gamepad)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(noButton.gameObject);
            }
        }

        public void SelectYesUi()
        {
            Application.Quit();
        }

        public async void SelectNoUi()
        {
            await modalHistoryController.Remove("Close");
        }
    }
}
