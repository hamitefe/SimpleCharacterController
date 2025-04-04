using System;
using UnityEngine;

namespace HamitEfe.SCC
{
    public class FloatingCharacter : CharacterComponent
    {
        /// <summary>
        /// the desired distance between the ground and character
        /// </summary>
        [Tooltip("Sensor")]
        public float desiredHeight;
        /// <summary>
        /// The distance to detect the ground
        /// </summary>
        public float sensorDistance;
        /// <summary>
        /// The settings of the spring force
        /// </summary>
        [Tooltip("Spring Properties")]
        public float 
            spring = 600,
            damping = 10;
        /// <summary>
        /// Should it push character downwards to snap to ground
        /// </summary>
        public bool applyNegativeForce = true;
        
        private Rigidbody rb;
        private CapsuleCollider capsule;
        public bool Grounded => ground != null;

        private GameObject ground;
        private GameObject stopSnap;

        private void Awake()
        {
            base.Awake();
            Debug.Log(Character);
            rb = GetComponent<Rigidbody>();
            capsule = GetComponentInChildren<CapsuleCollider>();
        }

        public void StopSnap()
        {
            stopSnap = ground;
        }

        private void FixedUpdate()
        {
            if (!Physics.SphereCast(transform.position+Vector3.up*capsule.radius,capsule.radius , Vector3.down, out RaycastHit hit, sensorDistance))
            {
                
                rb.linearVelocity += Physics.gravity * Time.deltaTime;
                if (ground) GroundExitCallbacks(ground);
                ground = null;
                stopSnap = null;
                return;
            }

            if (ground != hit.collider.gameObject)
            {
                if (ground) GroundExitCallbacks(ground);
                GroundEnterCallbacks(hit.collider.gameObject);
            }
            ground = hit.collider.gameObject;
            if (ground == stopSnap)
                return;
            SnapToGround(hit.distance);

            GroundReaction(hit.collider, hit.point);
        }

        private void GroundEnterCallbacks(GameObject gobject)
        {
            if (!gobject.TryGetComponent(out ICharacterGroundCallbacks callbacks)) return;
            callbacks.CharacterEnter(Character);
        }

        private void GroundExitCallbacks(GameObject gobject)
        {
            if (!gobject.TryGetComponent(out ICharacterGroundCallbacks callbacks)) return;
            callbacks.CharacterExit(Character);
        }

        private void GroundReaction(Collider ground, Vector3 hitPoint)
        {
            if (ground.attachedRigidbody==null) return;
            ground.attachedRigidbody.AddForceAtPosition(Vector3.down*Mathf.Abs(rb.linearVelocity.y)*rb.mass, hitPoint, ForceMode.Force);
        }

        private void SnapToGround(float hitDistance)
        {
            

            float distance = hitDistance;
            float displacement = distance - desiredHeight;
            float force = -spring * displacement - damping * rb.linearVelocity.y;
            if (force <= 0 && !applyNegativeForce) return;
            rb.linearVelocity += force * Time.deltaTime * Vector3.up;
        }
    }
}