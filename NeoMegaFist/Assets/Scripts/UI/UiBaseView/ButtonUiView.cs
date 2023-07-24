using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;
using Ui.SelectedUiFrame;

namespace Ui.UiBaseView
{
    [RequireComponent(typeof(Button))]
    public class ButtonUiView : MonoBehaviour
    {
        [Inject]
        private ISelectedUiFrameControllable selectedFrame;

        private void Awake()
        {
            Button button = GetComponent<Button>();
            RectTransform buttonRect = button.transform as RectTransform;
            
            button.OnSelectAsObservable()
                .Subscribe(_ => selectedFrame.SetFramePos(AnchorPresets.MiddleCenter, buttonRect.anchoredPosition, buttonRect.anchorMax, buttonRect.anchorMin, buttonRect.sizeDelta, button.transform.parent))
                .AddTo(gameObject);

            button.OnPointerEnterAsObservable()
                .Subscribe(_ => selectedFrame.SetFramePos(AnchorPresets.MiddleCenter, buttonRect.anchoredPosition, buttonRect.anchorMax, buttonRect.anchorMin, buttonRect.sizeDelta, button.transform.parent))
                .AddTo(gameObject);

            button.OnPointerExitAsObservable()
                .Subscribe(_ => selectedFrame.FrameEnable(false))
                .AddTo(gameObject);
        }

        //private void OnDestroy()
        //{
        //    selectedFrame.ResetFrameParent();
        //}
    }
}
