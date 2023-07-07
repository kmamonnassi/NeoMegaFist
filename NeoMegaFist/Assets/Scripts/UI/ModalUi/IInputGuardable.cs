namespace Ui.Modal
{
    interface IInputGuardable
    {
        /// <summary>
        /// モーダルがアニメーション中か？
        /// </summary>
        public bool isAnimationProp { get; }

        /// <summary>
        /// クリックを防ぐCanvasを有効無効化する
        /// </summary>
        /// <param name="enable">有効、無効</param>
        public void InputGuardEnable(bool enable);
    }
}
