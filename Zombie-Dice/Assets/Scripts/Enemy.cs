using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float movementSpeed;
    [SerializeField] protected float damage;
    [SerializeField] private float range;
    [SerializeField] private float undirectMovementStrength = 1f;

    [Space]
    [SerializeField] private AudioClip takeDamageClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip attackImpactClip;

    protected Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector2 offset = Vector2.zero;
    private float hp;

    protected bool attacking = false;
    protected bool canMove = true;

    void Start()
    {
        hp = maxHP;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        InvokeRepeating(nameof(GenerateOffset), 0f, .25f);
    }

    private void FixedUpdate()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        if (!canMove)
            return;

        Vector2 dir = Character.instance.transform.position - transform.position;
        dir.Normalize();
        dir += offset * undirectMovementStrength;
        dir.Normalize();

        rb.AddForce(40 * movementSpeed * Time.fixedDeltaTime * dir);
        transform.up = rb.velocity;
        animator.SetFloat("speed", rb.velocity.magnitude);
    }

    private void Attack()
    {
        if (attacking || !PlayerIsClose(0.8f))
            return;

        SoundManager.instance.Play(attackClip, 0.7f);

        StartCoroutine(ExecuteAttack());
    }

    private bool PlayerIsClose(float scale = 1f)
    {
        return (Character.instance.transform.position - transform.position).magnitude < range * scale;
    }


    public void TakeDamage(float value)
    {
        hp -= value;

        if (hp > 0)
            SoundManager.instance.Play(takeDamageClip, 0.7f);
        StartCoroutine(FlashRed());

        if (hp <= 0)
            Die();
    }

    private void Die()
    {
        SoundManager.instance.Play(deathClip);
        Destroy(gameObject);
    }

    private void GenerateOffset()
    {
        offset = UnityEngine.Random.insideUnitCircle;
    }

    private IEnumerator FlashRed()
    {
        const float duration = 0.1f;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Tweens.SmoothStop(Mathf.Clamp01(time / duration));
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
            yield return null;
        }
        while (time > 0)
        {
            time -= Time.deltaTime;
            float t = Tweens.SmoothStop(Mathf.Clamp01(time / duration));
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
            yield return null;
        }
    }

    protected virtual IEnumerator ExecuteAttack()
    {
        attacking = true;
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(.5f);

        if (PlayerIsClose())
        {
            Character.instance.TakeDamage(damage);
            SoundManager.instance.Play(attackImpactClip);
            canMove = false;
        }

        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(.5f);
        canMove = true;

        yield return new WaitForSeconds(.5f);
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, range * 0.8f);
    }

    public void Footsteps(AudioClip clip)
    {
        SoundManager.instance.Play(clip, 0.05f);
    }
}
