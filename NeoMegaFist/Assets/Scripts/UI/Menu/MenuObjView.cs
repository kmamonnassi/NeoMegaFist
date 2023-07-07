using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ui.Modal;

namespace Ui.Menu
{
    public class MenuObjView : ModalUiViewBase
    {
        void Start()
        {
            SetStateEvents("Open");
        }
    }
}
