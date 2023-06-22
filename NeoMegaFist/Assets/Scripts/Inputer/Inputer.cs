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

        public Vector2 GetPlayerMove()
        {
            return inputControl.Player.Move.ReadValue<Vector2>();
        }

        public bool GetPlayerCatch()
        {
            return inputControl.Player.Catch.triggered;
        }

        public bool GetPlayerThrow()
        {
            return inputControl.Player.Throw.triggered;
        }

        public bool GetPlayerThrowPreparation()
        {
            return inputControl.Player.ThrowPreparation.triggered;
        }
    }
}

