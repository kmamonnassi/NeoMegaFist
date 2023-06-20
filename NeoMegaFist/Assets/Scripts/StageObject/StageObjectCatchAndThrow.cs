using System;
using UnityEngine;

namespace StageObject
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class StageObjectCatchAndThrow : MonoBehaviour
    {
        [SerializeField] private ThrownCollider thrownCollider;
        [SerializeField] private bool isCatchableObject = true;
        
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
                character.OnEndStun += Released;
            }
        }

        public void Catched()
        {
            if (!isCatchableObject || IsCatched) return;

            IsCatched = true;
            OnCatched?.Invoke();
            rb.simulated = false;
            thrownCollider.enabled = false;
        }

        public void Released()
        {
            if (!isCatchableObject || !IsCatched) return;

            IsCatched = false;
            OnReleased?.Invoke();
            rb.simulated = true;
            thrownCollider.enabled = false;
        }

        public void Thrown(Vector2 dir, float power)
        {
            if (!isCatchableObject || IsThrown) return;

            IsThrown = true;
            OnThrown?.Invoke();
            rb.simulated = true;
            thrownCollider.enabled = true;

            rb.AddForce(dir * power, ForceMode2D.Impulse);
        }

        public void EndThrown()
        {
            if (!isCatchableObject || !IsThrown) return;

            IsThrown = false;
            OnEndThrown?.Invoke();
            thrownCollider.enabled = false;
        }
    }
}
