using UnityEngine;

namespace InputControl
{
    public class Inputer : IInputer
    {
        private InputControls inputControl;

        public Inputer()
        {
            inputControl = new InputControls();
            inputControl.Enable();
        }

        public Vector2 GetMousePosition()
        {
            return inputControl.Position.Position.ReadValue<Vector2>();
        }

        public Vector2 GetPlayerMove()
        {
            return inputControl.Player.Move.ReadValue<Vector2>();
        }

        public bool GetPlayerCatch()
        {
            return inputControl.Player.Catch.triggered;
        }

        public bool GetPlayerThrowStart()
        {
            return inputControl.Player.Throw.WasPressedThisFrame();
        }

        public bool GetPlayerThrow()
        {
            return inputControl.Player.Throw.IsPressed();
        }

        public bool GetPlayerThrowEnd()
        {
            return inputControl.Player.Throw.WasReleasedThisFrame();
        }

        public bool GetPlayerPunchStart()
        {
            return inputControl.Player.Punch.WasPressedThisFrame();
        }

        public bool GetPlayerPunch()
        {
            return inputControl.Player.Punch.IsPressed();
        }

        public bool GetPlayerPunchEnd()
        {
            return inputControl.Player.Punch.WasReleasedThisFrame();
        }
    }
}

