using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBox : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private List<int> outcomes = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Reroll();
    }

    private void Reroll()
    {
        
    }
}
