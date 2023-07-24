using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ui.Modal;
using Zenject;
using InputControl;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        public void SelectNoUi()
        {
            modalHistoryController.Remove("Close");
        }
    }
}
