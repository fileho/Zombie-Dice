using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Rigidbody2D projectile;

    protected override IEnumerator ExecuteAttack()
    {
        attacking = true;
        canMove = false;
        rb.velocity = Vector2.zero;

        Vector2 dir = Vector2.zero;
        float time = 0;
        while (time < 0.25f)
        {
            time += Time.deltaTime;
            dir = Character.instance.transform.position - transform.position;
            dir.Normalize();
            transform.up = Vector2.Lerp(transform.up, dir, 0.1f);
        }

        var o = Instantiate(projectile, transform.position + transform.up * 0.5f, Quaternion.identity);
        o.transform.up = dir;
        Vector2 force = projectileSpeed * 10 * dir;
        o.AddForce(force);
        o.GetComponent<Bullet>().Setup(damage);

        yield return new WaitForSeconds(.5f);
        rb.AddForce(force * 2);
        canMove = true;

        yield return new WaitForSeconds(2f);

        attacking = false;
    }
}
