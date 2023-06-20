using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace StageObject
{
    public class PlayerRotater : MonoBehaviour, IUpdate
    {
        private List<IPlayerRotate> rotates = new List<IPlayerRotate>();

        private Tween moveTween;

        public void Add(IPlayerRotate rotate)
        {
            rotates.Add(rotate);
        }

        public void Remove(IPlayerRotate rotate)
        {
            rotates.Remove(rotate);
        }

        public void ManagedUpdate()
        {
            IPlayerRotate rotate = null;
            foreach (IPlayerRotate r in rotates)
            {
                if (r.IsActive)
                {
                    if (rotate == null) rotate = r;
                    if (r.Priority > rotate.Priority)
                    {
                        rotate = r;
                    }
                }
            }
            if (rotate == null) return;

            moveTween?.Kill();
            moveTween = transform.DORotate(new Vector3(0, 0, rotate.Rotation), 0.15f);
        }
    }

    public interface IPlayerRotate
    {
        int Priority { get; }
        float Rotation { get; }
        bool IsActive { get; }
    }
}