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
    public float HealthRegenDelay;

    [Header("Grounded Check")]
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    public Vector3 EntityVelocity;

    public bool IsGrounded;
    public bool IsDead;

    private bool _isTakingDamage;

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
        CheckDeath();
        _isTakingDamage = true;
        StartCoroutine(CheckHealthRegen());
    }

    private IEnumerator CheckHealthRegen()
    {
        float prevHealth = CurrentHealth;
        yield return new WaitForSeconds(HealthRegenDelay);
        if (prevHealth == CurrentHealth)
        {
            _isTakingDamage = false;
            StartCoroutine(RegenHealth());
        }
    }

    private IEnumerator RegenHealth()
    {
        float healthToRegen = (MaxHealth - CurrentHealth);
        for (int i = 0; i < healthToRegen; i++)
        {
            if (CurrentHealth + i >= MaxHealth)
            {
                CurrentHealth = MaxHealth;
                break;
            }
            if (_isTakingDamage)
            {
                break;
            }
            CurrentHealth += i;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0)
        {
            IsDead = true;
        }
    }
}
