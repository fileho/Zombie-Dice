using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] GunStats gunStats;

    public List<Attachment> atts;

    private List<Attachment> attachments = new List<Attachment>();

    private float shootDelay = 0f;
    private Rigidbody2D rb;

    private Transform barrel;

    public static Gun instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        barrel = transform.Find("Barrel");

        rb = GetComponentInParent<Rigidbody2D>();

        // Create a new instance
        gunStats = Instantiate(gunStats);
    }

    // Update is called once per frame
    void Update()
    {
        ReadyShoot();

        if (CanShoot())
            Shoot();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            AddAttachment(atts[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            AddAttachment(atts[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            AddAttachment(atts[2]);        
        if (Input.GetKeyDown(KeyCode.Alpha4))
            AddAttachment(atts[3]);
    }

    private bool CanShoot()
    {
        return (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && shootDelay <= 0f && gunStats.CanShoot();
    }

    private void ReadyShoot()
    {
        if (shootDelay > 0)
            shootDelay -= Time.deltaTime * gunStats.rateOfFire;
    }

    private void Shoot()
    {
        for (int i = 0; i < gunStats.bulletCount; i++)
        {
            // shotgun
            float j = (i + 1) / 2;
            float dir = i % 2 == 0 ? 1 : -1;
            float angle = j * dir * 8 * Mathf.Deg2Rad;

            Vector2 direction = RotateVector(transform.up, angle);

            var o = Instantiate(bullet, barrel.position, Quaternion.identity);
            o.transform.up = direction;

            var force = 10 * gunStats.bulletSpeed * direction;
            o.AddForce(force);
            o.GetComponent<Bullet>().Setup(gunStats.damage, gunStats.explosionRange);

            // Conservation of energy
            rb.AddForce(-force);
        }

        gunStats.Shoot();
        shootDelay = 0.5f;
    }

    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        Vector2 ret = Vector2.zero;
        ret.x = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
        ret.y = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);
        return ret;
    }

    public void AddAttachment(Attachment attachment)
    {
        const int size = 3;
        if (attachments.Count == size)
            RemoveAttachment();
        attachments.Add(attachment);
        attachments[attachments.Count - 1].Apply(gunStats);
    }

    private void RemoveAttachment()
    {
        attachments[0].Remove(gunStats);
        attachments.RemoveAt(0);
    }
}
