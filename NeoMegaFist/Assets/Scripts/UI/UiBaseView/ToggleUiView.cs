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
    [RequireComponent(typeof(Toggle))]
    public class ToggleUiView : MonoBehaviour
    {
        [Inject]
        private ISelectedUiFrameControllable selectedFrame;
        
        private void Awake()
        {
            Toggle toggle = GetComponent<Toggle>();
            RectTransform toggleRect = toggle.transform as RectTransform;

            toggle.OnSelectAsObservable()
                .Subscribe(_ => selectedFrame.SetFramePos(AnchorPresets.MiddleCenter, toggleRect.anchoredPosition, toggleRect.anchorMax, toggleRect.anchorMin, toggleRect.sizeDelta, toggleRect.transform.parent))
                .AddTo(gameObject);

            toggle.OnPointerEnterAsObservable()
                .Subscribe(_ => selectedFrame.SetFramePos(AnchorPresets.MiddleCenter, toggleRect.anchoredPosition, toggleRect.anchorMax, toggleRect.anchorMin, toggleRect.sizeDelta, toggleRect.transform.parent))
                .AddTo(gameObject);

            toggle.OnPointerExitAsObservable()
                .Subscribe(_ => selectedFrame.SetFramePos(AnchorPresets.MiddleCenter, toggleRect.anchoredPosition, toggleRect.anchorMax, toggleRect.anchorMin, toggleRect.sizeDelta, toggleRect.transform.parent))
                .AddTo(gameObject);
        }
    }
}
