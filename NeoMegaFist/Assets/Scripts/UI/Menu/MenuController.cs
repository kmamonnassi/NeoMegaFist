using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject menuObj;

        private GameObject makedMenuObj = null;

        [Inject]
        private DiContainer container;

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
