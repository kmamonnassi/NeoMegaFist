using UnityEngine;

namespace InputControl
{
    public interface IInputer
    {
        Vector2 GetPlayerMove();
        Vector2 GetMousePosition();
        bool GetPlayerCatch();
        bool GetPlayerThrowStart();
        bool GetPlayerThrow();
        bool GetPlayerThrowEnd();
        bool GetPlayerPunchStart();
        bool GetPlayerPunch();
        bool GetPlayerPunchEnd();
    }
}