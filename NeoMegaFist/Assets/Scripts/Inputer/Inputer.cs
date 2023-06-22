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

        public bool GetOverhandThrowStart()
        {
            return inputControl.Player.OverhandThrow.WasPerformedThisFrame();
        }

        public bool GetOverhandThrow()
        {
            return inputControl.Player.OverhandThrow.IsPressed();
        }

        public bool GetOverhandThrowEnd()
        {
            return inputControl.Player.OverhandThrow.WasReleasedThisFrame();
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

        public bool GetPlayerDodgeStart()
        {
            return inputControl.Player.Dodge.WasPerformedThisFrame();
        }

        public bool GetPlayerDodge()
        {
            return inputControl.Player.Dodge.IsPressed();
        }

        public bool GetPlayerDodgeEnd()
        {
            return inputControl.Player.Dodge.WasReleasedThisFrame();
        }
    }
}

