using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using InputControl;
using Ui.Modal;
using Utility;

namespace Ui.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject menuObj;

        [Inject]
        private IInputer inputer;

        [Inject]
        private IInputGuardable inputGuardable;

        [Inject]
        private IModalHistoryControllable modalHistory;

        private GameObject makedMenuObj = null;

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

                    await modalHistory.RemoveAll();
                    makedMenuObj = null;
                }
            }
        }
    }
}
