using UnityEngine;
using Zenject;

namespace StageObject
{
    public class TestEnemy_2 : CharacterBase
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float shotInterval = 2;
        [SerializeField] private float bulletSpeed = 200;
        [SerializeField] private float bulletLifeTime = 10;
        [SerializeField] private ObjectSearcher searcher;
        [SerializeField] private bool isAlreadySearched = false;

        public override StageObjectID ID => StageObjectID.Mushroom;
        public override StageObjectType Type => StageObjectType.Enemy;
        public override Size DefaultSize => Size.Small;

        [Inject] private DiContainer container;
        [Inject] private Player player;

        private float nowShotInterval = 0;

        protected override void OnAwake_Virtual()
        {
            base.OnAwake_Virtual();
            searcher.OnSearched += (obj) => isAlreadySearched = true;
        }

        protected override void OnUpdate_Virtual()
        {
            base.OnUpdate_Virtual();
            if (IsStun) return;
            if (player.IsKilled) return;
            if (!isAlreadySearched) return;

            nowShotInterval += Time.deltaTime;
            if(nowShotInterval > shotInterval)
            {
                nowShotInterval = 0;
                Shot(((Vector2)(player.transform.position - transform.position)).normalized);
            }
        }

        private void Shot(Vector2 dir)
        {
            Bullet bullet = container.InstantiatePrefab(bulletPrefab, transform.position, Quaternion.identity, null).GetComponent<Bullet>();
            bullet.gameObject.SetActive(true);
            bullet.SetVelocity(dir * bulletSpeed);
		}
    }
}