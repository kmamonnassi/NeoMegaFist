using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Title
{
    public class TitleUiModel
    {
        /// <summary>
        /// ゲームをはじめから遊ぶ
        /// </summary>
        public void StartGame()
        {
            Debug.Log("ゲームをはじめから遊ぶ");
        }

        /// <summary>
        /// 途中からゲームを遊ぶ
        /// </summary>
        public void LoadGame()
        {
            Debug.Log("途中からゲームを遊ぶ");
        }
    }
}
