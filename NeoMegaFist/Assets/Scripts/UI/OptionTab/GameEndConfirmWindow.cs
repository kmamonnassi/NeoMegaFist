using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ui.Modal;
using Zenject;

namespace Ui.Option
{
    // TODO:���쐬�̃N���X
    public class GameEndConfirmWindow : MonoBehaviour
    {
        [Inject]
        private IModalHistoryControllable modalHistoryController;

        void Start()
        {
        
        }

        void Update()
        {
        
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
