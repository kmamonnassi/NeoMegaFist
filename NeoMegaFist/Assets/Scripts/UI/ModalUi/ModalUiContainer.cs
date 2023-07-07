using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Ui.Modal
{
    public class ModalUiContainer : MonoBehaviour, IModalHistoryControllable, IInputGuardable
    {
        [Inject]
        private DiContainer diContainer;

        [SerializeField]
        private GameObject inputGuardObj;

        [SerializeField]
        private GameObject backGroundInputGuardObj;

        private GameObject makedBackGroundInputGuardObj = null;

        private bool isAnimation = false;
        public bool isAnimationProp => isAnimation;

        private List<ModalUiViewBase> modalUiList = new List<ModalUiViewBase>();


        GameObject IModalHistoryControllable.Add(GameObject makeObj, Transform rootTransform, string enterStateName)
        {
            GameObject makedObj = diContainer.InstantiatePrefab(makeObj, rootTransform);

            ModalUiViewBase modalUiViewBase = makedObj.GetComponent<ModalUiViewBase>();
            modalUiList.Add(modalUiViewBase);

            modalUiViewBase.PlayOpenAnimation(enterStateName);

            if(makedBackGroundInputGuardObj == null)
            {
                makedBackGroundInputGuardObj = Instantiate(backGroundInputGuardObj, rootTransform.parent);
            }

            // クリックを防止する板を生成したオブジェクトの背面に配置する
            int makedObjSiblingIndex = makedObj.transform.GetSiblingIndex();
            makedBackGroundInputGuardObj.transform.SetParent(rootTransform.parent);
            makedBackGroundInputGuardObj.transform.SetSiblingIndex(makedObjSiblingIndex);
            var makedBackGroundInputGuardObjRectTrans = makedBackGroundInputGuardObj.transform as RectTransform;
            makedBackGroundInputGuardObjRectTrans.anchoredPosition = Vector3.zero;

            return makedObj;
        }

        async UniTask IModalHistoryControllable.Remove(string exitStateName, string targetModalName)
        {
            if (string.IsNullOrEmpty(targetModalName))
            {
                ModalUiViewBase listLast = modalUiList.Last();
                int listLastIndex = modalUiList.Count - 1;

                await listLast.PlayCloseAnimation(exitStateName);
                if (listLastIndex == 0)
                {
                    Destroy(makedBackGroundInputGuardObj);
                }
                else
                {
                    // クリックを防止する板を一つ前の履歴の背面に配置する
                    int beforeModalIndex = modalUiList[listLastIndex - 1].gameObject.transform.GetSiblingIndex();
                    makedBackGroundInputGuardObj.transform.SetParent(modalUiList[listLastIndex - 1].gameObject.transform.parent);
                    makedBackGroundInputGuardObj.transform.SetSiblingIndex(beforeModalIndex);
                    var makedBackGroundInputGuardObjRectTrans = makedBackGroundInputGuardObj.transform as RectTransform;
                    makedBackGroundInputGuardObjRectTrans.anchoredPosition = Vector3.zero;
                }
                
                Destroy(listLast.gameObject);
                modalUiList.Remove(listLast);
            }
            else
            {
                // targetまで一気に戻れる機能
            }
        }

        async UniTask IModalHistoryControllable.RemoveAll()
        {
            string exitStateName = "Close";
            UniTask[] tasks = new UniTask[modalUiList.Count];
            for (int i = 0; i < modalUiList.Count; i++)
            {
                ModalUiViewBase modalUiView = modalUiList[i];
                tasks[i] = modalUiView.PlayCloseAnimation(exitStateName);
            }
            await UniTask.WhenAll(tasks);

            for (int i = 0; i < modalUiList.Count; i++)
            {
                Destroy(modalUiList[i].gameObject);
            }
            Destroy(makedBackGroundInputGuardObj);
            modalUiList.Clear();
        }

        void IInputGuardable.InputGuardEnable(bool enable)
        {
            inputGuardObj.SetActive(enable);
            isAnimation = enable;
        }
    }
}
