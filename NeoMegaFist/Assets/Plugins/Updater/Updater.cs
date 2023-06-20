using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
	public class Updater : MonoBehaviour, IUpdater
	{
		private List<IUpdate> updates = new List<IUpdate>();
		private List<IFixedUpdate> fixedUpdates = new List<IFixedUpdate>();

		private void Update()
		{
			foreach (IUpdate update in updates)
			{
				update.ManagedUpdate();
			}
		}

		private void FixedUpdate()
		{
			foreach (IFixedUpdate update in fixedUpdates)
			{
				update.ManagedFixedUpdate();
			}
		}

		public void AddUpdate(IUpdate update)
		{
			updates.Add(update);
		}

		public void RemoveUpdate(IUpdate update)
		{
			updates.Remove(update);
		}

		public void AddFixedUpdate(IFixedUpdate fixedUpdate)
		{
			fixedUpdates.Add(fixedUpdate);
		}

		public void RemoveFixedUpdate(IFixedUpdate fixedUpdate)
		{
			fixedUpdates.Remove(fixedUpdate);
		}
	}
}