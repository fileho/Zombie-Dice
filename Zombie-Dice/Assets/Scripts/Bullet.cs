using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explosion;

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

        Destroy(gameObject);
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
