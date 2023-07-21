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
        private GameObject frameObj;

        private const float offsetSize = 10f;

        private void Start()
        {
            frameObj.SetActive(false);
        }

        void ISelectedUiFrameControllable.SetFramePos(Vector2 pos, Vector2 size, Transform root)
        {
            frameObj.transform.SetParent(root);

            RectTransform rect = frameObj.transform as RectTransform;
            rect.anchoredPosition = pos;

            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x + offsetSize);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y + offsetSize);

            frameObj.SetActive(true);
        }

        void ISelectedUiFrameControllable.FrameEnable(bool enable)
        {
            frameObj.SetActive(enable);
        }

        public void SetFirstSelectedUi(Selectable selectableUi)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selectableUi.gameObject);
            RectTransform rect = selectableUi.transform as RectTransform;
            ((ISelectedUiFrameControllable)this).SetFramePos(rect.anchoredPosition, rect.sizeDelta, selectableUi.transform.parent);
            ((ISelectedUiFrameControllable)this).FrameEnable(true);
        }
    }
}
