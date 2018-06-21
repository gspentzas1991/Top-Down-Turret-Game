using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The mode of the turret is saved on the GameMode PlayerPrefs int, and is described as : 0 for Mouse mode, 1 for Keyboard Mode and 2 for AI mode
 * During Mouse Mode the turret head will turn towards the mouse
 * During Keyboard mode the turret will turn with the Q and E keys
 * During AI mode the turret picks targets, turns to face them and fires at on its own.
 * When the turrethead fires, it creates a bullet. This behaviour is overriden by scripts of other weapon types
 */
public class TurretHead : MonoBehaviour
{
    //The target the Turret picked while it is in AI mode
    GameObject AITarget;
    //The speed at which the turret fires while in AI mode
    public float AITurretCooldown;
    float currentAIcooldown;
    public GameObject bullet;

    private void Start()
    {
        currentAIcooldown = AITurretCooldown;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("GameMode")==2)
        {
            //While in AI mode the turret only fires when its cooldown is over
            if (currentAIcooldown > 0)
                currentAIcooldown -= Time.deltaTime;
            else
                AIControl();
        }
        else
        {
            if (PlayerPrefs.GetInt("GameMode") == 0)
                FollowMouse();
            else
                KeyboardControl();
            if ((Input.GetMouseButtonDown(0)) || (Input.GetKeyDown(KeyCode.Space)))
            {
                FireProjectile();
            }
        }
    }

    //Makes the turretHead rotate towards the cursor
    void FollowMouse()
    {
        //Creates a ray from the mouse screen position, to the main camera's world position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Creates a plane that represents the ground
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        //Finds the point where the ray hits the plane, and turns the turret to look at it
        plane.Raycast(ray, out distance);
        Vector3 pointToLookAt = ray.GetPoint(distance);
        transform.LookAt(pointToLookAt);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void KeyboardControl()
    {
        if (Input.GetKey(KeyCode.Q))
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 2, 0);
        if (Input.GetKey(KeyCode.E))
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 2, 0);
    }

    //In AI mode, If the turret finds an enemy, it turns to them, fires and starts its cooldown
    void AIControl()
    {
        if (AITarget != null)
        {
            transform.LookAt(AITarget.transform);
            FireProjectile();
            currentAIcooldown = AITurretCooldown;
        }
    }
    
    //Creates a bullet at the turret's position. This method is overiden by different types of turret heads
    public virtual void FireProjectile()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    //Only used in AI mode. When a skeleton enters the trigger it becomes targeted until it dies
    private void OnTriggerStay(Collider other)
    {
        if ((AITarget == null) && (other.gameObject.tag == "Enemy"))
        {
            AITarget = other.gameObject;
        }
    }

}
