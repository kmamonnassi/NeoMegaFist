using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using InputControl;
using Cysharp.Threading.Tasks;
using Ui.Modal;

namespace Ui.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject menuObj;

        [Inject]
        private DiContainer container;

        [Inject]
        private IInputer inputer;

        [Inject]
        private IInputGuardable inputGuardable;

        [Inject]
        private IModalHistoryControllable modalHistory;

        private GameObject makedMenuObj = null;
        private MenuObjView menuView;

        private void Awake()
        {
            Locator<BeforeSelectedSettingKind>.Bind(new BeforeSelectedSettingKind());
        }

        void Start()
        {

        }

        async void Update()
        {
            if (inputer.GetPlayerMenuStart() && !inputGuardable.isAnimationProp)
            {
                if (makedMenuObj == null)
                {
                    AudioReserveManager.AudioReserve("MenuUI", "メニューを開く音", transform);
                    
                    makedMenuObj = modalHistory.Add(menuObj, transform, "Open");
                }
                else
                {
                    AudioReserveManager.AudioReserve("MenuUI", "メニューを閉じる音", transform);
                    menuView = makedMenuObj.GetComponent<MenuObjView>();

                    //await modalHistory.Remove("Close", "");
                    await modalHistory.RemoveAll();
                    makedMenuObj = null;
                }
            }
        }
    }
}
