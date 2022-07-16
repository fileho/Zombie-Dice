using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;


    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 dir = Character.instance.transform.position - transform.position;
        dir.Normalize();

        rb.AddForce(40 * movementSpeed * Time.fixedDeltaTime * dir);
    }

    public void TakeDamage(float value)
    {
        hp -= value;

        StartCoroutine(FlashRed());

        if (hp <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator FlashRed()
    {
        const float duration = 0.1f;
        float time = 0;

        while (time < duration)
        {
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < duration)
        {
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
