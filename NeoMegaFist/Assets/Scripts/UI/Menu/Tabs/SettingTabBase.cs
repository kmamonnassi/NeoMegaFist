using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Menu
{
    public class SettingTabBase : MonoBehaviour
    {
        [SerializeField]
        private SettingKinds settingKind;
        public SettingKinds settingKindProp => settingKind;

        protected bool isSaved = false;

        /// <summary>
        /// タブの中の設定項目を保存する
        /// </summary>
        public virtual void SaveSettingData()
        {
        }
    }
}
