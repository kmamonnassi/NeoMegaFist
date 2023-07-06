using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

namespace StageObject
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class StageObjectCatchAndThrow : MonoBehaviour, IStageObjectCatchAndThrow
    {
        [SerializeField] private ThrownCollider thrownCollider;
        [SerializeField] private bool isCatchableObject = true;//このオブジェクトは掴めるかどうか
        [Inject] private OverhandThrownImpact impactPrefab;

        public ThrownState State { get; private set; } = ThrownState.Freedom;
        public bool IsCatchableObject => isCatchableObject;
        public ThrownCollider ThrownCollider => thrownCollider;

		public event Action OnCatched;
        public event Action OnReleased;
        public event Action OnThrown;
        public event Action OnEndThrown;
        public event Action<Vector2, float> OnOverhandThrown;
        public event Action OnEndOverhandThrown;

        private Rigidbody2D rb;
        private float slowTime;

        private readonly AnimationCurve overhandThrownScale = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.2f, 1.5f),
            new Keyframe(0.5f, 2f),
            new Keyframe(0.8f, 1.5f),
            new Keyframe(1f, 0f)
        );

        private readonly AnimationCurve overhandThrownPosition = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(1f, 1f)
        );

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            CharacterBase character = GetComponent<CharacterBase>();
            if (character != null)
            {
                //掴まれた状態でスタンが終了すると離れる
                character.OnEndStun += Released;
            }
            if (thrownCollider != null)
                thrownCollider.Initalize(rb);
        }

        private void Update()
        {
            if (State == ThrownState.Throw)
            {
                if (rb.velocity.magnitude < 30)
                {
                    slowTime += Time.deltaTime;
                    if (slowTime > 0.1f)
                    {
                        //投げられた状態で速度が遅くなった時、投げを終了する
                        EndThrown();
                        slowTime = 0;
                    }
                }
                else
                {
                    slowTime = 0;
                }
            }
        }

        /// <summary>掴まれたとき</summary>
		public void Catched()
        {
            if (!isCatchableObject || State == ThrownState.Catch) return;

            State = ThrownState.Catch;
            OnCatched?.Invoke();
            rb.simulated = false;
            thrownCollider.gameObject.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("ThrownStageObject");
        }

        /// <summary>掴みが解除されたとき</summary>
        public void Released()
        {
            if (!isCatchableObject || State != ThrownState.Catch) return;

            State = ThrownState.Freedom;
            OnReleased?.Invoke();
            rb.simulated = true;
            gameObject.layer = LayerMask.NameToLayer("StageObject");
            AudioReserveManager.AudioReserve("敵", "この敵のスタンが解除されて掴みが解除された", transform);
        }

        /// <summary>投げるとき</summary>
        public void Thrown(Vector2 dir, float power)
        {
            if (!isCatchableObject || State == ThrownState.Throw) return;
            Released();//投げるときは掴み状態が解除される
            State = ThrownState.Throw;
            OnThrown?.Invoke();
            //投げられるときは攻撃用のコライダーをオンにし、それ以外をオフにする
            thrownCollider.gameObject.SetActive(true);
            thrownCollider.SetState(ThrownState.Throw);
            gameObject.layer = LayerMask.NameToLayer("ThrownStageObject");

            //投げの威力と方向をもとに、力を加える
            rb.AddForce(dir * power, ForceMode2D.Impulse);
        }

        public void EndThrown()
        {
            if (!isCatchableObject || State != ThrownState.Throw) return;

            State = ThrownState.Freedom;
            OnEndThrown?.Invoke();
            thrownCollider.gameObject.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("StageObject");
        }

        public void OverhandThrown(Vector2 position, float duration)
        {
            if (!isCatchableObject || State == ThrownState.OverhandThrow) return;
            Released();
            State = ThrownState.OverhandThrow;
            gameObject.layer = LayerMask.NameToLayer("ThrownStageObject");
            OnOverhandThrown?.Invoke(position, duration);

            Vector3 beforePos = transform.position;
            Vector3 nowPosition = Vector3.zero;

            Vector3 additiveScale = transform.localScale;
            Vector3 beforeScale = Vector3.zero;

            DOVirtual.Float(0, 1, duration, x =>
            {
                nowPosition = Vector2.Lerp(beforePos, new Vector2(position.x, position.y), overhandThrownPosition.Evaluate(x));
                transform.position = new Vector3(nowPosition.x, nowPosition.y, -9);

                transform.localScale -= beforeScale;
                beforeScale = additiveScale * overhandThrownScale.Evaluate(x);
                transform.localScale += beforeScale;

                transform.localScale -= beforeScale;
                beforeScale = additiveScale * overhandThrownScale.Evaluate(x);
                transform.localScale += beforeScale;
            })
            .SetEase(Ease.Linear)
            .onComplete += () => 
            {
                OnEndOverhandThrown?.Invoke();
                State = ThrownState.Freedom;
                transform.position = new Vector3(nowPosition.x, nowPosition.y, 0);

                thrownCollider.SetState(ThrownState.OverhandThrow);
                thrownCollider.gameObject.SetActive(true);

                OverhandThrownImpact impact = Instantiate(impactPrefab, transform.position, Quaternion.identity, null);
                impact.Initalize(thrownCollider);

                DOVirtual.DelayedCall(0.1f, () => 
                {
                    thrownCollider.gameObject.SetActive(false);
                    gameObject.layer = LayerMask.NameToLayer("StageObject");
                    Destroy(impact.gameObject);
                });
                AudioReserveManager.AudioReserve("ステージにあるオブジェクト", "上投げをされたオブジェクトが着地", transform);
            };
        }
    }

    public enum ThrownState
    {
        Freedom,
        Catch,
        Throw,
        OverhandThrow
    }
}
