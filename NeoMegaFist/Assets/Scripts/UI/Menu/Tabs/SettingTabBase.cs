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
        /// �^�u�̒��̐ݒ荀�ڂ�ۑ�����
        /// </summary>
        public virtual void SaveSettingData()
        {
        }
    }
}
