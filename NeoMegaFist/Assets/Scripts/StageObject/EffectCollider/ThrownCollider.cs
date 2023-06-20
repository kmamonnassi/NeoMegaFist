using UnityEngine;

namespace StageObject
{
    public class ThrownCollider : EffectCollider
    {
#if UNITY_EDITOR
        [SerializeField] private Collider2D attackCollider;
        [SerializeField] private Collider2D wallHitCollider;

        private void Awake()
        {
            if (attackCollider == null)
                Debug.LogError("敵に衝突するコライダーが存在しません。");
            else
            if (wallHitCollider.isTrigger)
                Debug.LogError("敵に衝突するコライダーのIsTriggerをTrueにしてください。貫通しなくなります。");

            if (wallHitCollider == null)
                Debug.LogError("壁に反射するコライダーが存在しません。");
            else
            if (wallHitCollider.isTrigger)
                Debug.LogError("壁に反射するコライダーのIsTriggerを外してください。反射しなくなります。");
        }
#endif
    }
}
