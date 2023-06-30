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
        /// ƒ^ƒu‚Ì’†‚Ìİ’è€–Ú‚ğ•Û‘¶‚·‚é
        /// </summary>
        public virtual void SaveSettingData()
        {
        }
    }
}
