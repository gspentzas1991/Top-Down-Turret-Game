using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This trigger collider is used when the turret is operation in AI mode. 
 * When a skeleton enters the trigger, the skeleton gets saved as a target.
 * The Turret gets the target info from the SendTarget() method
 */
public class TurretTrigger : MonoBehaviour {
    
    GameObject target;
	
    private void OnTriggerStay(Collider other)
    {
        if ((target == null)&&(other.gameObject.tag=="Enemy"))
        {
            target = other.gameObject;
        }
    }

    public GameObject SendTarget()
    {
        return target;
    }
}
