using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Ui.Modal
{
    interface IModalHistoryControllable
    {
        /// <summary>
        /// モーダルを追加
        /// </summary>
        /// <param name="makeObj">生成するモーダルオブジェクト</param>
        /// <param name="rootTransform">親となるTransform</param>
        /// <param name="enterStateName">生成時のアニメーションのStateの名前</param>
        public GameObject Add(GameObject makeObj, Transform rootTransform, string enterStateName);

        /// <summary>
        /// モーダルを消す
        /// </summary>
        /// <param name="exitStateNamem">消すときのアニメーションのStateの名前</param>
        /// <param name="targetModalName">戻りたいモーダルの名前</param>
        public UniTask Remove(string exitStateName, string targetModalName = "");

        /// <summary>
        /// 履歴を全部消す
        /// </summary>
        public UniTask RemoveAll();
    }
}
