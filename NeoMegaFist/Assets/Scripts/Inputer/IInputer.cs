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
        bool GetOverhandThrowStart();
        bool GetOverhandThrow();
        bool GetOverhandThrowEnd();
        bool GetPlayerPunchStart();
        bool GetPlayerPunch();
        bool GetPlayerPunchEnd();
        bool GetPlayerDodgeStart();
        bool GetPlayerDodge();
        bool GetPlayerDodgeEnd();
        public ControllerType GetControllerType();
        bool GetPlayerMenu();
    }
}