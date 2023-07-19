using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

namespace Ui.Title
{
    public class TitleUiPresenter : IInitializable
    {
        [Inject]
        private TitleUiView view;

        [Inject]
        private TitleUiModel model;

        public void Initialize()
        {
            view.loadButtonClickHandler.Subscribe(_ => model.LoadGame());
            view.startButtonClickHandler.Subscribe(_ => model.StartGame());
        }
    }
}
