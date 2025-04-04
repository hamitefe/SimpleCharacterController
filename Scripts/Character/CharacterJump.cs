using System;
using UnityEngine;

namespace HamitEfe.SCC
{
    public class CharacterJump : CharacterComponent
    {
        private FloatingCharacter character;
        private ICharacterInputProvider inputProvider;
        private Rigidbody rb;
        public float jumpForce = 10f;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            character = GetComponent<FloatingCharacter>();
            inputProvider = GetComponent<ICharacterInputProvider>();
        }

        private void Update()
        {
            if (!CanJump()) return;
            character.StopSnap();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        private bool CanJump()
        {
            return inputProvider.IsJumping && character.Grounded;
        }
    }
}