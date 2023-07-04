using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using InputControl;

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

        private GameObject makedMenuObj = null;
        private MenuObjView menuView;

        void Start()
        {

        }

        void Update()
        {
            if(inputer.GetPlayerMenuStart())
            {
                if(makedMenuObj == null)
                {
                    AudioReserveManager.AudioReserve("MenuUI", "���j���[���J����", transform);
                    makedMenuObj = container.InstantiatePrefab(menuObj, transform);
                    menuView = makedMenuObj.GetComponent<MenuObjView>();
                    menuView.PlayOpenAnimation();
                }
                else
                {
                    AudioReserveManager.AudioReserve("MenuUI", "���j���[����鉹", transform);
                    menuView = makedMenuObj.GetComponent<MenuObjView>();
                    menuView.PlayCloseAnimation();
                    //Destroy(makedMenuObj);
                }
            }
        }
    }
}
