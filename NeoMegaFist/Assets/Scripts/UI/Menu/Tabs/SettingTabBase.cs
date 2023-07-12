using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Menu
{
    public class SettingTabBase : MonoBehaviour
    {
        [SerializeField]
        private SettingKinds settingKind;
        public SettingKinds settingKindProp => settingKind;

        protected bool isSaved = false;

        /// <summary>
        /// ƒ^ƒu‚Ì’†‚Ìİ’è€–Ú‚ğ•Û‘¶‚·‚é
        /// </summary>
        public virtual void SaveSettingData()
        {
        }
    }
}
