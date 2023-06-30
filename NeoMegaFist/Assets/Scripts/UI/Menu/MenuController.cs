using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Ui.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject menuObj;

        [Inject]
        private DiContainer container;

        private GameObject makedMenuObj = null;

        void Start()
        {
            
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(makedMenuObj == null)
                {
                    makedMenuObj = container.InstantiatePrefab(menuObj, transform);
                }
                else
                {
                    Destroy(makedMenuObj);
                }
            }
        }
    }
}
