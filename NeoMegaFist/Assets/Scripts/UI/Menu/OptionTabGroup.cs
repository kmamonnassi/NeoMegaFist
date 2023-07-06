using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Ui.Menu
{
    public class OptionTabGroup : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> tabObjs;
        
        [Inject]
        private DiContainer container;

        private BeforeSelectedSettingKind beforeSelectedSettingKinds;

        private Dictionary<SettingKinds, GameObject> tabDic = new Dictionary<SettingKinds, GameObject>();

        private GameObject makedTabObj = null;

        private void Awake()
        {
            beforeSelectedSettingKinds = Locator<BeforeSelectedSettingKind>.GetT();

            foreach (var tabObj in tabObjs)
            {
                SettingTabBase settingTab = tabObj.GetComponent<SettingTabBase>();
                if (!tabDic.ContainsKey(settingTab.settingKindProp))
                {
                    tabDic.Add(settingTab.settingKindProp, tabObj);
                }
            }
        }

        private void Start()
        {
            SettingKinds targetSettingKind = beforeSelectedSettingKinds.selectSettingKind;
            ShowTab(targetSettingKind);
        }

        /// <summary>
        /// タブを表示する
        /// </summary>
        /// <param name="settingKind">タブの種類</param>
        public void ShowTab(SettingKinds settingKind)
        {
            if (makedTabObj != null)
            {
                HideTab();
            }
            makedTabObj = container.InstantiatePrefab(tabDic[settingKind], transform);

            beforeSelectedSettingKinds.selectSettingKind = settingKind;
        }

        /// <summary>
        /// タブを非表示にする
        /// </summary>
        public void HideTab()
        {
            SaveTabData();

            Destroy(makedTabObj);
        }

        private void OnDestroy()
        {
            SaveTabData();
        }

        /// <summary>
        /// セーブする
        /// </summary>
        private void SaveTabData()
        {
            SettingTabBase tab = makedTabObj.GetComponent<SettingTabBase>();
            tab.SaveSettingData();
        }
    }
}
