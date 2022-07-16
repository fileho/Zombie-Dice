using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Explosion explosion;

    [Space]
    [SerializeField] private AudioClip impactClip;

    private float damage = 0;
    private float explosionRange = 0;

    public void Setup(float damage, float explosionRange, float mass)
    {
        this.damage = damage;
        this.explosionRange = explosionRange;

        GetComponent<Rigidbody2D>().mass *= mass;

        float scale = 1 + explosionRange * 0.2f;
        transform.localScale *= scale;

        Invoke(nameof(End), 8f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosionRange > 0)
            Explode();

        DealDamage(collision);
        if (explosionRange == 0)
            SoundManager.instance.Play(impactClip);

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
        o.Setup(damage, explosionRange);
    }

    private void End()
    {
        Destroy(gameObject);
    }
}
