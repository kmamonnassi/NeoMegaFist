using UnityEngine;

namespace Ui.InputGuardCanvas
{
    public class UiInputGuardCanvas : MonoBehaviour, IInputGuardable
    {
        /// <summary>
        /// 最前面に何も描画しないキャンバスを配置してクリックされるのを防ぐ
        /// </summary>
        /// <param name="enable">有効、無効化</param>
        public void InputGuardEnable(bool enable)
        {
            this.gameObject.SetActive(enable);
        }
    }
}
