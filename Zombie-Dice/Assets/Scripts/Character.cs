using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float maxArmor;
    [SerializeField] private float movementSpeed;

    [Space]
    [SerializeField] private AudioClip takeDamageClip;
    [SerializeField] private AudioClip deathClip;


    private float hp;
    private float armor;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private int interactable = 0;

    public static Character instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        UpdateHPbar();
        UpdateArmorbar();
    }

    private void FixedUpdate()
    {
        if (interactable > 0)
            return;

        Movement();
        Rotate();
    }

    public bool IsInteractable()
    {
        return interactable == 0;
    }

    public void SetInteractable(int value)
    {
        interactable += value;
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

        StartCoroutine(FlashRed());
        SoundManager.instance.Play(takeDamageClip);

        hp -= value;

        hp = Mathf.Max(hp, 0);
        UpdateHPbar();

        if (hp <= 0)
            Die();
    }

    public void RestoreHP(float value)
    {
        hp += value;
        hp = Mathf.Min(hp, maxHP);
        UpdateHPbar();
    }

    private void UpdateHPbar()
    {
        Healthbar.instance.UpdateHealthbar(hp / maxHP);
    }

    private void UpdateArmorbar()
    {
        Healthbar.instance.UpdateArmorbar(Mathf.Max(0, armor) / maxArmor);
    }

    public void AddArmor(float value)
    {
        armor += value;
        armor = Mathf.Min(armor, maxHP);

        UpdateArmorbar();
    }


    // returns leftovers damage
    private float ConsumeArmor(float value)
    {
        if (armor == 0)
            return value;

        armor -= value;
        UpdateArmorbar();

        if (armor >= 0)
            return 0;

        float leftover = -armor;
        armor = 0;
        return leftover;
    }

    private void Die()
    {
        SoundManager.instance.Play(deathClip);

        interactable++;
        Gameover.instance.Activate();
    }

    public void PlayFootsteps(AudioClip clip) {
        SoundManager.instance.Play(clip, 0.5f); 
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
