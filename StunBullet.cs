using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherits Bullet. When the stun bullet hits a skeleton, it stuns it for a set number of seconds
public class StunBullet : Bullet
{
    public float stunTime;
    override public void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Enemy")
        {
            Skeleton targetSkeleton = target.gameObject.GetComponent<Skeleton>();
            targetSkeleton.GotHit(damage, stunTime);
            Destroy(this.gameObject);
        }
    }
}
