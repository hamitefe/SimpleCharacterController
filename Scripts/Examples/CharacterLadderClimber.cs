using System;
using UnityEngine;

public class CharacterLadderClimber : CharacterComponent
{
    private bool climbing = false;
    [SerializeField]
    private float climbingSpeed = .5f;
    [SerializeField]
    private float acceleration = 10f;

    [SerializeField]
    private float maxHorizontalCounter = 10f;
    private new void Awake()
    {
        base.Awake();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("LadderBeggining")) return;
        if (!climbing) return;
        
        SetClimbingState(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ladder")) return;
        SetClimbingState(true);
    }

    private void FixedUpdate()
    {
        if (!climbing) return;
        float movement = Character.InputProvider.Movement.z;
        float targetVelocity = movement * climbingSpeed;
        float currentVelocity = Character.Rigidbody.linearVelocity.y;
        float delta = targetVelocity - currentVelocity;
        delta = Mathf.Clamp(delta, -acceleration, acceleration);
        Vector3 horizontal = Vector3.Scale(Character.Rigidbody.linearVelocity, -Vector3.one+Vector3.up);
        Vector3 horizontalDelta = Vector3.ClampMagnitude(horizontal,maxHorizontalCounter);
        Debug.Log(horizontalDelta);
        Character.Rigidbody.AddForce(horizontalDelta + Vector3.up*delta, ForceMode.VelocityChange);
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.CompareTag("Ladder")) return;
        SetClimbingState(false);

    }

    private void SetClimbingState(bool flag)
    {
        climbing = flag;
        Character.MovementManager.enabled = !flag;
        Character.FloatingManager.enabled = !flag;
        Character.JumpManager.enabled = !flag;
        Character.Rigidbody.useGravity = !flag;
    }
}
