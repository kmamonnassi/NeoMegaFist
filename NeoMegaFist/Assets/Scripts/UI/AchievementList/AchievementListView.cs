using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.AchievementList
{
    public class AchievementListView : MonoBehaviour
    {
        [SerializeField]
        private GameObject achievementUiObj;

        [SerializeField]
        private Transform contentTrans;

        void Start()
        {
            for (int i = 0; i < 100; i++)
            {
                Instantiate(achievementUiObj, contentTrans);
            }
        }

        void Update()
        {
        
        }
    }

}
