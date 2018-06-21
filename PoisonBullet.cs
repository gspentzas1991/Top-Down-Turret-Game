using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Inherits the Bullet script. When it hits an enemy it damages him and causes poison damage over time. The poison bullet gets destroyed afterwards.
 */
public class PoisonBullet : Bullet
{
    public int poisonDamageOverTime;

	override public void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Enemy")
        {
            Skeleton targetSkeleton = target.gameObject.GetComponent<Skeleton>();
            targetSkeleton.GotHit(damage,poisonDamageOverTime);
            Destroy(this.gameObject);
        }
    }
}
