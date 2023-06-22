using UnityEngine;

namespace StageObject
{
    public interface IPlayerMove
    {
        int MovePriority { get; }
        bool MoveIsActive { get; }
        Vector2 MoveVelocity { get; }
    }
}