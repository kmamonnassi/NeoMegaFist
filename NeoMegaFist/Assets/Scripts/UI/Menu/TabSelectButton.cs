using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ui.Menu
{
    public class TabSelectButton : MonoBehaviour
    {
        [SerializeField]
        private OptionKinds optionKind;

        [SerializeField]
        private Button button;

        [SerializeField]
        private OptionTabGroup tabGroup;

        void Start()
        {
            button.OnClickAsObservable()
                .Subscribe(_ => tabGroup.ShowTab(optionKind))
                .AddTo(this.gameObject);
        }
    }
}
