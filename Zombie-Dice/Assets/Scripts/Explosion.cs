using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem ps;

    private float damage;

    public void Setup(float damage, float radius)
    {
        this.damage = damage;

        var col = GetComponent<CircleCollider2D>();
        transform.localScale = new Vector3(radius, radius, radius);
        GetComponent<Collider2D>().enabled = true;

        ps = GetComponent<ParticleSystem>();
        ps.Play();

        Invoke(nameof(End), 0.6f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        var e = collision.GetComponent<Enemy>();
        e.TakeDamage(damage);
    }

    private void End()
    {
        Destroy(gameObject);
    }
}
