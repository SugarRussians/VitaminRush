using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float BulletVelocity;
    public float Damage;

    private Rigidbody _rigidbody;
    private Vector3 _defaultPosition;

    void Start()
    {
        _defaultPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.velocity = transform.forward * BulletVelocity;

        // TODO make bullet drop
        if (Vector3.Distance(_defaultPosition, transform.position) > 100)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        BaseEntity baseEntity = collision.gameObject.GetComponent<BaseEntity>();
        print(collision.gameObject.name);
        if (baseEntity)
        {
            baseEntity.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}
