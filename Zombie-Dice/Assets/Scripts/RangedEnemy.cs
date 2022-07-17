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
       
        Vector2 dir = Character.instance.transform.position - transform.position;
        dir.Normalize();
        transform.up = dir;

        float time = 0;
        while (time < 0.25f)
        {
            time += Time.deltaTime;
            dir = Character.instance.transform.position - transform.position;
            dir.Normalize();
            transform.up = dir;
        }

        var o = Instantiate(projectile, transform.position, Quaternion.identity);
        o.transform.up = dir;
        Vector2 force = projectileSpeed * 10 * dir;
        o.AddForce(force);
        o.GetComponent<Bullet>().Setup(damage);

        rb.AddForce(-force);


        yield return new WaitForSeconds(.5f);
        canMove = true;

        yield return new WaitForSeconds(2f);

        attacking = false;
    }
}
