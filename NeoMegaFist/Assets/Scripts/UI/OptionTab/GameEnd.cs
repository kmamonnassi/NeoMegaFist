using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ui.Modal;
using Zenject;

namespace Ui.Option
{
    public class GameEnd : MonoBehaviour
    {
        [Inject]
        private IModalHistoryControllable modalHistoryController;

        [SerializeField]
        private GameObject gameEndConfirmObj;

        void Start()
        {
        
        }

        public void GameEndConfirm()
        {
            modalHistoryController.Add(gameEndConfirmObj, transform, "Open");
        }
    }
}
