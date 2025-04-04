using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace HamitEfe.SCC
{
    public class SimpleInputProvider : MonoBehaviour,ICharacterInputProvider
    {
        public InputActionReference
            move,
            look,
            jump;

        private void OnEnable()
        {
            move.action.Enable();
            look.action.Enable();
            jump.action.Enable();
        }

        private void OnDisable()
        {
            move.action.Disable();
            look.action.Disable();
            jump.action.Disable();
        }

        public Vector3 Movement
        {
            get
            {
                Vector2 vec2 = move.action.ReadValue<Vector2>();
                return new(vec2.x, 0, vec2.y);
            }
        }

        public Vector3 Look
        {
            get
            {
                Vector2 vec2 = look.action.ReadValue<Vector2>();
                return new(-vec2.y, vec2.x, 0);
            }
        }

        public bool IsJumping => jump.action.WasPressedThisFrame();
    }
}