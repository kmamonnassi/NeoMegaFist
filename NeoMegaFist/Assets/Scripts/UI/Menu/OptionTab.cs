using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Menu
{
    public class OptionTab : MonoBehaviour
    {
        [SerializeField]
        private OptionKinds optionKind;
        public OptionKinds optionKindProp => optionKind;

        protected bool isSaved = false;

        /// <summary>
        /// �^�u�̒��̐ݒ荀�ڂ�ۑ�����
        /// </summary>
        public virtual void SaveSettingData()
        {
        }
    }
}
