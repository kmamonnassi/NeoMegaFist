using UnityEngine;
using UnityEngine.UI;

namespace Ui.SelectedUiFrame
{
    interface ISelectedUiFrameControllable
    {
        /// <summary>
        /// 選択中の枠を移動させ、適切なサイズに自動調整する
        /// </summary>
        /// <param name="pos">位置</param>
        /// <param name="size">大きさ</param>
        /// <param name="root">親のTransform</param>
        public void SetFramePos(Vector2 pos, Vector2 size, Transform root);
        
        /// <summary>
        /// 枠を表示するか
        /// </summary>
        /// <param name="enable">表示、非表示</param>
        public void FrameEnable(bool enable);

        /// <summary>
        /// 最初に選んでる状態になっているUIを変える
        /// </summary>
        /// <param name="selectableUi">選択可能なUI</param>
        public void SetFirstSelectedUi(Selectable selectableUi);
    }
}