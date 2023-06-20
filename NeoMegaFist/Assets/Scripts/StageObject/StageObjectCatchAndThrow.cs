using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace StageObject
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class StageObjectCatchAndThrow : MonoBehaviour
    {
        [SerializeField] private ThrownCollider thrownCollider;
        [SerializeField] private bool isCatchableObject = true;//このオブジェクトは掴めるかどうか
        
        public bool IsCatched { get; private set; }
        public bool IsThrown { get; private set; }
        public bool IsCatchableObject => isCatchableObject;

        public event Action OnCatched;
        public event Action OnReleased;
        public event Action OnThrown;
        public event Action OnEndThrown;

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            CharacterBase character = GetComponent<CharacterBase>();
            if(character != null)
            {
                //掴まれた状態でスタンが終了すると離れる
                character.OnEndStun += Released;
            }
        }

		private void Update()
		{
			if(IsThrown)
            {
                if(rb.velocity.magnitude < 10)
                {
                    //投げられた状態で速度が遅くなった時、投げを終了する
                    EndThrown();
				}
			}
		}

        /// <summary>掴まれたとき</summary>
		public void Catched()
        {
            if (!isCatchableObject || IsCatched) return;

            IsThrown = false;
            IsCatched = true;
            OnCatched?.Invoke();
            rb.simulated = false;
            thrownCollider.gameObject.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("ThrownStageObject");
        }

        /// <summary>掴みが解除されたとき</summary>
        public void Released()
        {
            if (!isCatchableObject || !IsCatched) return;

            IsThrown = false;
            IsCatched = false;
            OnReleased?.Invoke();
            rb.simulated = true;
            gameObject.layer = LayerMask.NameToLayer("StageObject");
        }

        /// <summary>投げるとき</summary>
        public void Thrown(Vector2 dir, float power)
        {
            if (!isCatchableObject || IsThrown) return;
            
            Released();//投げるときは掴み状態が解除される
            IsThrown = true;
            OnThrown?.Invoke();
            //投げられるときは攻撃用のコライダーをオンにし、それ以外をオフにする
            thrownCollider.gameObject.SetActive(true);
            gameObject.layer = LayerMask.NameToLayer("ThrownStageObject");

            //投げの威力と方向をもとに、力を加える
            rb.AddForce(dir * power, ForceMode2D.Impulse);
        }

        public void EndThrown()
        {
            if (!isCatchableObject || !IsThrown) return;
            
            IsCatched = false;
            IsThrown = false;
            OnEndThrown?.Invoke();
            thrownCollider.gameObject.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("StageObject");
        }
    }
}
