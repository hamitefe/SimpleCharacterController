using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace HamitEfe.SCC
{
    public class CharacterMovement : CharacterComponent
    {
        /// <summary>
        /// The desired speed of the character
        /// </summary>
        public float speed;
        /// <summary>
        /// Maximum change of velocity per physics tick
        /// </summary>
        public float acceleration;
        /// <summary>
        /// acceleration in air
        /// </summary>
        public float airAcceleration = 1;

        private ICharacterInputProvider inputProvider;
        private FloatingCharacter character;
        private Rigidbody rb;

        private void Awake()
        {
            character = GetComponent<FloatingCharacter>();
            rb = GetComponent<Rigidbody>();
            inputProvider = GetComponent<ICharacterInputProvider>();
        }

        private void FixedUpdate()
        {
            Vector3 input = inputProvider.Movement;
            if (!character.Grounded && input.sqrMagnitude <= .01f) return;
            float acceleration = character.Grounded ? this.acceleration : airAcceleration;
            Vector3 targetMovement = speed * Time.deltaTime * transform.TransformDirection(inputProvider.Movement);
            Vector3 currentMovement = Vector3.Scale(rb.linearVelocity, Vector3.one - Vector3.up);
            Vector3 delta = targetMovement - currentMovement;
            Vector3 force = Vector3.ClampMagnitude(delta, acceleration * Time.deltaTime);
            rb.AddForce(force, ForceMode.VelocityChange);
        }

        private void OnDrawGizmos()
        {
            if (rb == null) return;
            Gizmos.DrawRay(transform.position, Vector3.Scale(rb.linearVelocity, Vector3.one - Vector3.up));
        }
    }
}