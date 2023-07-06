using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

namespace Ui.Modal
{
    public class ModalUiViewBase : MonoBehaviour
    {
        [SerializeField]
        protected string modalName = string.Empty;
        public string modalNameProp => modalName;

        [Inject]
        private IInputGuardable inputGuard;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected void SetStateEvents(string enterStateName, string exitStateName)
        {
            ObservableStateMachineTrigger trigger = animator.GetBehaviour<ObservableStateMachineTrigger>();

            trigger.OnStateEnterAsObservable()
                .Where(onStateInfo => onStateInfo.StateInfo.IsName(enterStateName))
                .Subscribe(_ => { inputGuard.InputGuardEnable(true);})
                .AddTo(gameObject);

            trigger.OnStateUpdateAsObservable()
                .Where(onStateInfo => onStateInfo.StateInfo.IsName(enterStateName))
                .Where(onStateInfo => onStateInfo.StateInfo.normalizedTime > 1.0)
                .Take(1)
                .Subscribe(_ => { inputGuard.InputGuardEnable(false); animator.SetBool(enterStateName, false);})
                .AddTo(gameObject);
        }

        public void PlayOpenAnimation(string enterStateName)
        {
            animator = GetComponent<Animator>();
            animator.SetBool(enterStateName, true);
        }

        public async UniTask PlayCloseAnimation(string exitStateName)
        {
            animator = GetComponent<Animator>();
            animator.SetBool(exitStateName, true);
            inputGuard.InputGuardEnable(true); 
            var token = this.GetCancellationTokenOnDestroy();
            await UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f, cancellationToken: token);
            inputGuard.InputGuardEnable(false);
        }
    }

}
