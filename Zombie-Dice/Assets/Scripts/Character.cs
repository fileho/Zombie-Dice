using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody2D rb;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
