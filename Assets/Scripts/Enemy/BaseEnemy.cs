using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseEntity
{
    [Header("Radius for checking ")]
    public float CheckRange;

    public bool PlayerInLineOfSight;

    public void CheckSurroundings()
    {
        GameObject player = GameObject.Find("Player");
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hitInfo, CheckRange))
        {
            if (hitInfo.transform.gameObject == player)
            {
                transform.LookAt(player.transform);
                PlayerInLineOfSight = hitInfo.collider.gameObject.name == player.gameObject.name;
            }
            else
            {
                PlayerInLineOfSight = false;
            }
        }
        else
        {
            PlayerInLineOfSight = false;
        }
    }

    public void CheckSelfDestroyWhenDead()
    {
        if (IsDead)
        {
            Destroy(gameObject);
        }
    }
}
