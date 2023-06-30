using UnityEngine;

namespace Ui.DisplayInteract
{
    interface IDisplayableInteractModel
    {
        /// <summary>
        /// インタラクトボタンのUIを表示する
        /// </summary>
        /// <param name="spriteAsset">表示するボタンのAsset</param>
        /// <param name="pos">表示位置</param>
        public void ShowUI(DisplayInteractSpriteAsset spriteAsset, Vector2 pos);
        
        /// <summary>
        /// UIを非表示にする
        /// </summary>
        public void HideUI();

        /// <summary>
        /// 表示位置を変更する
        /// </summary>
        /// <param name="pos">表示位置</param>
        public void SetPos(Vector2 pos);
    }
}