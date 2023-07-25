using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Utility;
using InputControl;
using System;
using Ui.Modal;

namespace Ui.Menu
{
    public class OptionTabGroup : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> tabObjs;

        [Inject]
        private IInputer inputer;
        
        [Inject]
        private DiContainer container;

        [Inject]
        private IModalHistoryControllable modalHistoryController;

        private BeforeSelectedSettingKind beforeSelectedSettingKinds;

        private Dictionary<SettingKinds, GameObject> tabDic = new Dictionary<SettingKinds, GameObject>();
        private SettingKinds[] settingKindArray;

        private GameObject makedTabObj = null;

        private int beforeSelectedSettingKindNum = 0;

        private void Awake()
        {
            beforeSelectedSettingKinds = Locator<BeforeSelectedSettingKind>.GetT();

            settingKindArray = new SettingKinds[tabObjs.Count];

            for (int i = 0; i < tabObjs.Count; i++)
            {
                GameObject tabObj = tabObjs[i];
                SettingTabBase settingTab = tabObj.GetComponent<SettingTabBase>();
                if (!tabDic.ContainsKey(settingTab.settingKindProp))
                {
                    tabDic.Add(settingTab.settingKindProp, tabObj);
                    settingKindArray[i] = settingTab.settingKindProp;
                }
            }
        }

        private void Start()
        {
            SettingKinds targetSettingKind = beforeSelectedSettingKinds.selectSettingKind;
            beforeSelectedSettingKindNum = Array.IndexOf(settingKindArray, targetSettingKind);
            ShowTab(targetSettingKind);
        }

        private void Update()
        {
            if (inputer.GetControllerType() == ControllerType.Gamepad)
            {
                if(inputer.GetTabChangeLeft())
                {
                    beforeSelectedSettingKindNum = beforeSelectedSettingKindNum - 1;
                    if(beforeSelectedSettingKindNum < 0)
                    {
                        beforeSelectedSettingKindNum = tabObjs.Count - 1;
                    }
                    ShowTab(settingKindArray[beforeSelectedSettingKindNum]);

                    beforeSelectedSettingKinds.selectSettingKind = settingKindArray[beforeSelectedSettingKindNum];
                }

                if(inputer.GetTabChangeRight())
                {
                    beforeSelectedSettingKindNum = (beforeSelectedSettingKindNum + 1) % tabObjs.Count;
                    ShowTab(settingKindArray[beforeSelectedSettingKindNum]);
                    
                    beforeSelectedSettingKinds.selectSettingKind = settingKindArray[beforeSelectedSettingKindNum];
                }
            }
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

            modalHistoryController.Remove("", "Menu");
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
