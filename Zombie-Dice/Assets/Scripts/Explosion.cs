using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float damage;

    public void Setup(float damage)
    {
        this.damage = damage;

        GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        var e = collision.GetComponent<Enemy>();
        e.TakeDamage(damage);
    }

}
