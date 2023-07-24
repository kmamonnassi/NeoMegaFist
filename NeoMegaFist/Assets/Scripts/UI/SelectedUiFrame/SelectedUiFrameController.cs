using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Ui.SelectedUiFrame
{
    // TODO:ƒNƒ‰ƒX–¼‚ð‚©‚¦‚½‚¢‚Ë
    public class SelectedUiFrameController : MonoBehaviour, ISelectedUiFrameControllable
    {
        [SerializeField]
        private GameObject frameObjPrefab;

        [SerializeField]
        private GameObject frameObj;

        private const float offsetSize = 10f;

        private RectTransform frameRect;
        private Transform frameParent;

        private void Awake()
        {
            frameRect = frameObj.transform as RectTransform;
        }

        private void Start()
        {
            frameObj.SetActive(false);
            frameParent = this.transform;
        }

        private void MakeFrame()
        {
            if(frameObj == null)
            {
                frameObj = Instantiate(frameObjPrefab);
                frameRect = frameObj.transform as RectTransform;
            }
        }

        void ISelectedUiFrameControllable.SetFramePos(AnchorPresets anchorPreset, Vector2 pos, Vector2 anchorMax, Vector2 anchorMin, Vector2 size, Transform root)
        {
            MakeFrame();
            frameObj.transform.SetParent(root);

            frameRect.SetAnchor(anchorPreset);

            frameRect.anchoredPosition = pos;

            frameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x + offsetSize);
            frameRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y + offsetSize);
            frameRect.localScale = Vector3.one;

            ChangeAnchor(anchorMax, anchorMin);

            frameObj.SetActive(true);
        }

        public void ChangeAnchor(Vector2 anchorMax, Vector2 anchorMin)
        {
            frameRect.anchorMax = anchorMax;
            frameRect.anchorMin = anchorMin;
        }

        void ISelectedUiFrameControllable.FrameEnable(bool enable)
        {
            frameObj.SetActive(enable);
        }

        //void ISelectedUiFrameControllable.SetFirstSelectedUi(Selectable selectableUi)
        //{
        //    EventSystem.current.SetSelectedGameObject(null);
        //    EventSystem.current.SetSelectedGameObject(selectableUi.gameObject);
        //    RectTransform rect = selectableUi.transform as RectTransform;
        //    ((ISelectedUiFrameControllable)this).SetFramePos(rect.anchoredPosition, rect.sizeDelta, selectableUi.transform.parent);
        //    ((ISelectedUiFrameControllable)this).FrameEnable(true);
        //}

        void ISelectedUiFrameControllable.ResetFrameParent()
        {
            Debug.Log("reset");
            frameObj.transform.SetParent(frameParent);
            frameRect.localScale = Vector3.one;
        }
    }
}
