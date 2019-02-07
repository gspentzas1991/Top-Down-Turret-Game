using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Shotgun turrethead fires three bullets, each 10 degrees away from each other.
 * It inherits TurretHead.
 */
public class TurretShotgun : TurretHead
{
    public override void FireProjectile()
    {
        Quaternion bulletRotation = transform.rotation;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(bullet, transform.position, bulletRotation);
            bulletRotation.eulerAngles = new Vector3(bulletRotation.eulerAngles.x, bulletRotation.eulerAngles.y + 10, bulletRotation.eulerAngles.z);
        }
    }
}
