using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using InputControl;
using Zenject;
using UnityEngine.EventSystems;
using UniRx.Triggers;

namespace Ui.Title
{
    public class TitleUiView : MonoBehaviour
    {
        [Inject]
        private IInputer input;

        [Inject]
        private Ui.SelectedUiFrame.ISelectedUiFrameControllable selectedFrame;

        [SerializeField]
        private Button loadButton;

        [SerializeField]
        private Button startButton;

        public Subject<Unit> loadButtonClickHandler = new Subject<Unit>();
        public Subject<Unit> startButtonClickHandler = new Subject<Unit>();

        private ControllerType beforeType;

        private void Awake()
        {
            loadButton.OnClickAsObservable()
                .Subscribe(_ => loadButtonClickHandler.OnNext(Unit.Default))
                .AddTo(gameObject);

            startButton.OnClickAsObservable()
                .Subscribe(_ => startButtonClickHandler.OnNext(Unit.Default))
                .AddTo(gameObject);
        }

        private void Start()
        {
            if (input.GetControllerType() == ControllerType.Gamepad)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(loadButton.gameObject);
            }
            beforeType = input.GetControllerType();
        }

        private void Update()
        {
            if (beforeType != input.GetControllerType())
            {
                if(input.GetControllerType() == ControllerType.Gamepad)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(loadButton.gameObject);
                }
                beforeType = input.GetControllerType();
            }
        }
    }
}
