using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Modal
{
    public class ModalUiView : ModalUiViewBase
    {
        [SerializeField]
        private string enterStateName;

        void Start()
        {
            SetStateEvents(enterStateName);
        }
    }
}
