using System;
using UnityEngine;

namespace StageObject
{
    public interface IStageObjectCatchAndThrow
    {
        ThrownState State { get; }
        bool IsCatchableObject { get; }
        ThrownCollider ThrownCollider { get; }

        event Action OnCatched;
        event Action OnReleased;
        event Action OnThrown;
        event Action OnEndThrown;

        void Catched();
        void Released();
        void Thrown(Vector2 dir, float power);
        void EndThrown();

    }
}
