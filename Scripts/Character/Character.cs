using System;
using UnityEngine;

namespace HamitEfe.SCC
{
    public class Character : MonoBehaviour
    {
        public CharacterJump JumpManager { get; private set; }
        public CharacterLook LookManager { get; private set;  }
        public CharacterMovement MovementManager { get; private set; }
        public FloatingCharacter FloatingManager { get; private set; }
        public ICharacterInputProvider InputProvider { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            JumpManager = GetComponent<CharacterJump>();
            LookManager = GetComponent<CharacterLook>();
            MovementManager = GetComponent<CharacterMovement>();
            FloatingManager = GetComponent<FloatingCharacter>();
            InputProvider = GetComponent<ICharacterInputProvider>();
        }
    }
}
