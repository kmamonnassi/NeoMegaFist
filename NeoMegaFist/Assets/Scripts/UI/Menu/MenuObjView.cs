using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Ui.InputGuardCanvas;
using Zenject;
using Cysharp.Threading.Tasks;

namespace Ui.Menu
{
    //TODO:アニメーションステートにEnterの時とExitした時の処理を書いた基底クラスを作るべき
    public class MenuObjView : MonoBehaviour
    {
        [Inject]
        private IInputGuardable inputGuard;

        private Animator animator;


        void Start()
        {
            ObservableStateMachineTrigger trigger = animator.GetBehaviour<ObservableStateMachineTrigger>();

            trigger.OnStateEnterAsObservable()
                .Where(onStateInfo => onStateInfo.StateInfo.IsName("Open"))
                .Subscribe(_ => inputGuard.InputGuardEnable(true))
                .AddTo(gameObject);

            trigger.OnStateUpdateAsObservable()
                .Where(onStateInfo => onStateInfo.StateInfo.IsName("Open"))
                .Where(onStateInfo => onStateInfo.StateInfo.normalizedTime > 1.0)
                .Take(1)
                .Subscribe(_ => { inputGuard.InputGuardEnable(false); animator.SetBool("Open", false); })
                .AddTo(gameObject);

            trigger.OnStateEnterAsObservable()
                .Where(onStateInfo => onStateInfo.StateInfo.IsName("Close"))
                .Subscribe(_ => inputGuard.InputGuardEnable(true))
                .AddTo(gameObject);

            trigger.OnStateUpdateAsObservable()
                .Where(onStateInfo => onStateInfo.StateInfo.IsName("Close"))
                .Where(onStateInfo => onStateInfo.StateInfo.normalizedTime > 1.0)
                .Take(1)
                .Subscribe(_ => { inputGuard.InputGuardEnable(false); animator.SetBool("Close", false); Destroy(this.gameObject); })
                .AddTo(gameObject);
        }

        public void PlayOpenAnimation()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("Open", true);
        }

        public async UniTask PlayOpenAnimationTask()
        {
            await UniTask.Delay(1);
        }

        public void PlayCloseAnimation()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("Close", true);
        }
    }
}
