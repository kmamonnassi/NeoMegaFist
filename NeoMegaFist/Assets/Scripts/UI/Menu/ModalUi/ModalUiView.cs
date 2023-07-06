using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Modal
{
    public class ModalUiView : ModalUiViewBase
    {
        [SerializeField]
        private string enterStateName;

        [SerializeField]
        private string exitStateName;

        void Start()
        {
            SetStateEvents(enterStateName, exitStateName);
        }
    }
}
