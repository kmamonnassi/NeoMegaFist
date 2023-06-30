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
        /// タブの中の設定項目を保存する
        /// </summary>
        public virtual void SaveSettingData()
        {
        }
    }
}
