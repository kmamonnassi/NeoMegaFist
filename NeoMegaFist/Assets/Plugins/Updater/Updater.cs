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
			List<IUpdate> copy = new List<IUpdate>(updates);
			foreach (IUpdate update in copy)
			{
				update.ManagedUpdate();
			}
		}

		private void FixedUpdate()
		{
			List<IFixedUpdate> copy = new List<IFixedUpdate>(fixedUpdates);
			foreach (IFixedUpdate update in copy)
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