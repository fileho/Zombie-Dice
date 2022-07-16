using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float maxArmor;
    [SerializeField] private float movementSpeed;

    private float hp;
    private float armor;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private Animator animator;

    public static Character instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        armor = maxArmor;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;        
    }

    private void FixedUpdate()
    {
        Movement();
        Rotate();
    }

    private void Movement()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
            dir += Vector2.right;
        if (Input.GetKey(KeyCode.A))
            dir += Vector2.left;
        if (Input.GetKey(KeyCode.W))
            dir += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            dir += Vector2.down;

        dir.Normalize();
        ExecuteMovement(dir);

        animator.SetFloat("speed", rb.velocity.magnitude);
    }

    private void ExecuteMovement(Vector2 dir)
    {
        rb.AddForce(200 * movementSpeed * Time.fixedDeltaTime * dir);
    }

    private void Rotate()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = mousePos - transform.position;
        dir.Normalize();

        transform.up = dir;
    }

    public void TakeDamage(float value)
    {
        value = ConsumeArmor(value);

        if (value == 0)
            return;

        hp -= value;
        if (hp <= 0)
            Die();
    }

    public void RestoreHP(float value)
    {
        hp += value;
        hp = Mathf.Min(hp, maxHP);
    }

    public void AddArmor(float value)
    {
        armor += value;
        armor = Mathf.Min(armor, maxHP);
    }


    // returns leftovers damage
    private float ConsumeArmor(float value)
    {
        if (armor == 0)
            return value;

        armor -= value;
        if (armor >= 0)
            return 0;

        float leftover = -armor;
        armor = 0;
        return leftover;
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}
