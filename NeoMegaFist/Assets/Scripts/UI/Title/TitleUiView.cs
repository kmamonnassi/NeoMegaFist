using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ui.Title
{
    public class TitleUiView : MonoBehaviour
    {
        [SerializeField]
        private Button loadButton;

        [SerializeField]
        private Button startButton;

        public Subject<Unit> loadButtonClickHandler = new Subject<Unit>();
        public Subject<Unit> startButtonClickHandler = new Subject<Unit>();

        private void Awake()
        {
            loadButton.OnClickAsObservable()
                .Subscribe(_ => loadButtonClickHandler.OnNext(Unit.Default))
                .AddTo(gameObject);

            startButton.OnClickAsObservable()
                .Subscribe(_ => startButtonClickHandler.OnNext(Unit.Default))
                .AddTo(gameObject);
        }
    }
}
