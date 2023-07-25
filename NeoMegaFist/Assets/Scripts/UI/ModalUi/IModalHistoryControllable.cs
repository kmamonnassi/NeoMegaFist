using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

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

        /// <summary>
        /// モーダルを消したときに選択するべきUIを登録する
        /// </summary>
        /// <param name="selectable">モーダルを消したときに選択するべきUI</param>
        public void RegisterSelectedUiWhenRemove(Selectable selectable);
    }
}
