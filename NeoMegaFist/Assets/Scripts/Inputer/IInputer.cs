using UnityEngine;

namespace InputControl
{
    public interface IInputer
    {
        Vector2 GetPlayerMove();
        bool GetPlayerCatch();
        bool GetPlayerThrow();
        bool GetPlayerThrowPreparation();
    }
}