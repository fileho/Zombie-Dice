using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Explosion explosion;

    private float damage = 0;
    private float explosionRange = 0;

    public void Setup(float damage, float explosionRange)
    {
        this.damage = damage;
        this.explosionRange = explosionRange;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosionRange > 0)
            Explode();

        DealDamage(collision);

        Destroy(gameObject);
    }

    private void DealDamage(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        var e = collision.gameObject.GetComponent<Enemy>();
        e.TakeDamage(damage);
    }

    private void Explode()
    {
        var o = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, -6), Quaternion.identity);
        o.Setup(damage);
    }
}
