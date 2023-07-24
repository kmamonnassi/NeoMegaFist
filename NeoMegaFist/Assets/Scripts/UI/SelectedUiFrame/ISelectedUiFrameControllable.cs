using UnityEngine;
using UnityEngine.UI;

namespace Ui.SelectedUiFrame
{
    interface ISelectedUiFrameControllable
    {
        /// <summary>
        /// 選択中の枠を移動させ、適切なサイズに自動調整する
        /// </summary>
        /// <param name="anchorPreset">アンカーのプリセット</param>
        /// <param name="pos">位置</param>
        /// <param name="anchorMax">anchorMax</param>
        /// <param name="anchorMin">anchorMin</param>
        /// <param name="size">大きさ</param>
        /// <param name="root">親のTransform</param>
        public void SetFramePos(AnchorPresets anchorPreset, Vector2 pos, Vector2 anchorMax, Vector2 anchorMin, Vector2 size, Transform root);

        /// <summary>
        /// アンカーを変える
        /// </summary>
        /// <param name="anchorMax">anchorMax</param>
        /// <param name="anchorMin">anchorMin</param>
        public void ChangeAnchor(Vector2 anchorMax, Vector2 anchorMin);
        
        /// <summary>
        /// 枠を表示するか
        /// </summary>
        /// <param name="enable">表示、非表示</param>
        public void FrameEnable(bool enable);

        ///// <summary>
        ///// 最初に選んでる状態になっているUIを変える
        ///// </summary>
        ///// <param name="selectableUi">選択可能なUI</param>
        //public void SetFirstSelectedUi(Selectable selectableUi);

        /// <summary>
        /// 枠の親オブジェクトをリセットする
        /// </summary>
        public void ResetFrameParent();

    }
}