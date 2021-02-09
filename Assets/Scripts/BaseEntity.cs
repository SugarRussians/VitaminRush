using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    [Header("Character Controller")]
    public CharacterController characterController;

    [Header("Entity Variables")]
    public float gravity;
    public float MaxHealth = 100;
    public float CurrentHealth;

    [Header("Grounded Check")]
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    public Vector3 EntityVelocity;

    public bool IsGrounded;

    public void InitializeCharacterController()
    {
        characterController = GetComponent<CharacterController>();
        CurrentHealth = MaxHealth;
    }

    public void CheckGrounded()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (IsGrounded && EntityVelocity.y < 0)
        {
            EntityVelocity.y = gravity;
        }

        if (characterController)
        {
            EntityVelocity.y += gravity * Time.deltaTime;
            characterController.Move(EntityVelocity * Time.deltaTime);
        }
    }

    public void TakeDamage(float damageTaken)
    {
        CurrentHealth -= damageTaken;
    }
}
