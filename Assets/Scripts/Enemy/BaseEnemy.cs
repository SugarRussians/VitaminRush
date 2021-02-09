using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseEntity
{
    [Header("Radius for checking ")]
    public float CheckRadius;

    public bool PlayerInLineOfSight;

    public void CheckSurroundings()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, CheckRadius);

        foreach (Collider collider in colliders)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player && CheckIfPlayerIsInLineOfSight(player))
            {
                PlayerInLineOfSight = true;
                break;
            }
            else
            {
                PlayerInLineOfSight = false;
            }
        }
    }

    private bool CheckIfPlayerIsInLineOfSight(PlayerController player)
    {
        transform.LookAt(player.transform);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo))
        {
            return hitInfo.collider.gameObject.name == player.gameObject.name;
        }
        return false;
    }

    public void CheckSelfDestroyWhenDead()
    {
        if (IsDead)
        {
            Destroy(gameObject);
        }
    }
}
