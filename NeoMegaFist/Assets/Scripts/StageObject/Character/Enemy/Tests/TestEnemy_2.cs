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
        [Inject] private DiContainer container;

        public override StageObjectID ID => StageObjectID.Mushroom;
        public override StageObjectType Type => StageObjectType.Enemy;
        public override Size DefaultSize => Size.Small;

        [Inject] private Player player;

        private float nowShotInterval = 0;

        protected override void OnUpdate_Virtual()
        {
            base.OnUpdate_Virtual();

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