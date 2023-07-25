using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ui.Modal;
using Zenject;
using UnityEngine.UI;

namespace Ui.OptionSetting
{
    public class GameEnd : MonoBehaviour
    {
        [Inject]
        private IModalHistoryControllable modalHistoryController;

        [SerializeField]
        private GameObject gameEndConfirmObj;

        [SerializeField]
        private Button button;

        void Start()
        {
        
        }

        public void GameEndConfirm()
        {
            modalHistoryController.RegisterSelectedUiWhenRemove(button);
            modalHistoryController.Add(gameEndConfirmObj, transform, "Open");
        }
    }
}
