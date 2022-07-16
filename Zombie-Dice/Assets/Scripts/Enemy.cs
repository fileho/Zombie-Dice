using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage;


    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(float value)
    {
        hp -= value;
        if (hp <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
