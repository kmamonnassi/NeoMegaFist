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
    [RequireComponent(typeof(Slider))]
    public class SliderUiView : MonoBehaviour
    {
        [Inject]
        private ISelectedUiFrameControllable selectedFrame;

        [SerializeField]
        private GameObject handleObj;
        
        private void Awake()
        {
            Slider slider = GetComponent<Slider>();
            RectTransform handleRect = handleObj.transform as RectTransform;

            slider.OnSelectAsObservable()
                .Subscribe(_ => selectedFrame.SetFramePos(AnchorPresets.VertStretchLeft, handleRect.anchoredPosition, handleRect.anchorMax, handleRect.anchorMin, handleRect.sizeDelta, handleObj.transform.parent))
                .AddTo(gameObject);

            slider.OnValueChangedAsObservable()
                .Skip(1)
                .Subscribe(_ => selectedFrame.SetFramePos(AnchorPresets.VertStretchLeft, handleRect.anchoredPosition, handleRect.anchorMax, handleRect.anchorMin, handleRect.sizeDelta, handleObj.transform.parent))
                .AddTo(gameObject);

            slider.OnPointerEnterAsObservable()
                .Subscribe(_ => selectedFrame.SetFramePos(AnchorPresets.VertStretchLeft, handleRect.anchoredPosition, handleRect.anchorMax, handleRect.anchorMin, handleRect.sizeDelta, handleObj.transform.parent))
                .AddTo(gameObject);

            slider.OnPointerExitAsObservable()
                .Subscribe(_ => selectedFrame.FrameEnable(false))
                .AddTo(gameObject);
        }

        //private void OnDestroy()
        //{
        //    selectedFrame.ResetFrameParent();
        //}
    }
}
