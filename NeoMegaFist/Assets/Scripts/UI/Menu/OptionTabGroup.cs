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

        [Inject]
        private BeforeSelectedOptionKind beforeSelectedOptionKinds;

        private Dictionary<OptionKinds, GameObject> tabDic = new Dictionary<OptionKinds, GameObject>();

        private GameObject makedTabObj = null;

        private void Awake()
        {
            foreach (var tabObj in tabObjs)
            {
                OptionTab optionTab = tabObj.GetComponent<OptionTab>();
                if (!tabDic.ContainsKey(optionTab.optionKindProp))
                {
                    tabDic.Add(optionTab.optionKindProp, tabObj);
                }
            }
        }

        private void Start()
        {
            OptionKinds targetOptionKind = beforeSelectedOptionKinds.selectOptionKind;
            ShowTab(targetOptionKind);
        }

        /// <summary>
        /// タブを表示する
        /// </summary>
        /// <param name="optionKind">タブの種類</param>
        public void ShowTab(OptionKinds optionKind)
        {
            if (makedTabObj != null)
            {
                HideTab();
            }
            makedTabObj = container.InstantiatePrefab(tabDic[optionKind], transform);

            beforeSelectedOptionKinds.selectOptionKind = optionKind;
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
            OptionTab tab = makedTabObj.GetComponent<OptionTab>();
            tab.SaveSettingData();
        }
    }
}
