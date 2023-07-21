using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;
using Ui.SelectedUiFrame;

namespace Ui
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
                .Subscribe(_ => selectedFrame.SetFramePos(buttonRect.anchoredPosition, buttonRect.sizeDelta, button.transform.parent))
                .AddTo(gameObject);

            button.OnPointerEnterAsObservable()
                .Subscribe(_ => selectedFrame.SetFramePos(buttonRect.anchoredPosition, buttonRect.sizeDelta, button.transform.parent))
                .AddTo(gameObject);
        }
    }
}
