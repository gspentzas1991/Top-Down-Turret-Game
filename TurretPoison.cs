using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Poison turrethead has a cooldown between shots. Its bullets poison the enemy and cause damage over time
 * It inherits TurretHead.
 */
public class TurretPoison : TurretHead
{
    public float firingSpeed;
    float currentShootCooldown=0;
    
    void FixedUpdate()
    {
        if (currentShootCooldown > 0)
            currentShootCooldown -= Time.deltaTime;
    }

    //When the turret fires, it creates a poison bullet and starts the cooldown period
    public override void FireProjectile()
    {
        if(currentShootCooldown<=0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            currentShootCooldown = firingSpeed;
        }
    }
    
}
