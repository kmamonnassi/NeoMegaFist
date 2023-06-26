using UnityEngine;
using Utility;
using Zenject;

namespace StageObject
{
	public class Bullet : MonoBehaviour, IUpdate
	{
		[SerializeField] private Rigidbody2D rb;
		[SerializeField] private float lifeTime = 10;
		[Inject] private IUpdater updater;

		private void Awake()
		{
			updater.AddUpdate(this);
		}

		public void SetVelocity(Vector2 velocity)
		{
			rb.velocity = velocity;
		}

		public void SetLifeTime(float lifeTime)
		{
			this.lifeTime = lifeTime;
		}

		public void ManagedUpdate()
		{
			lifeTime -= Time.deltaTime;
			if(lifeTime <= 0)
			{
				Remove();
			}
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.GetComponent<Wall>() != null)
			{
				Remove();
			}
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.GetComponent<Wall>() != null)
			{
				Remove();
			}
		}

		public void Remove()
		{
			updater.RemoveUpdate(this);
			Destroy(gameObject);
		}
	}
}