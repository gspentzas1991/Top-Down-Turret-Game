using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * When the skeleton spawns, it turns to look at the Turret and starts moving towards it. Bullets call its GotHit methods depending on their type, to damage it and apply effects
 * When the skeleton is poisoned, it gets damaged every second
 * When the skeleton is stunned, it stops moving for the duration
 * When the skeleton dies, it adds one point to the player's score
 * When the skeleton reaches the turret, it damages it and destroys itself.
 */
public class Skeleton : MonoBehaviour {

    public GameObject turret;
    public GameObject skeletonRenderer;
    public int meleeDamage;
    public int health;
    public float speed;
    float currentSpeed;
    bool isPoisoned = false;
    bool isStunned = false;
    int appliedDamageOverTime;
    float stunTimer=0;
    Rigidbody skeletonRigidbody;
    Animator skeletonAnimator;
    Material skeletonMaterial;

    private void Start()
    {
        //Ignores collision between skeletons
        Physics.IgnoreLayerCollision(8, 8);
        skeletonMaterial = skeletonRenderer.GetComponent<Renderer>().material;
        skeletonAnimator = GetComponent<Animator>();
        skeletonRigidbody = GetComponent<Rigidbody>();
        currentSpeed = speed;
        transform.LookAt(turret.transform);
    }

    void Update ()
    {
        if (isStunned)
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed = speed;
        }
        skeletonRigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * currentSpeed);
        skeletonAnimator.SetFloat("Speed", currentSpeed);
        if(isPoisoned)
        {
            StartCoroutine(ApplyPoisonDamage()) ;
        }
        if (health <= 0)
        {
            turret.GetComponent<Turret>().GainScore();
            Destroy(this.gameObject);
        }
    }

    //If the enemy is poisoned, he will get hurt every 1 second
    IEnumerator ApplyPoisonDamage()
    {
        yield return new WaitForSeconds(1);
        health -= appliedDamageOverTime;
    }

    //While the skeleton is stunned, it turns yellow for the duration, unless it is already poisoned in which case it stays green
    IEnumerator ApplyStun(float timeToStun)
    {
        isStunned = true;
        if(!isPoisoned)
            skeletonMaterial.SetColor("_Color", Color.yellow);
        yield return new WaitForSeconds(timeToStun);
        isStunned = false;
        if (!isPoisoned)
            skeletonMaterial.SetColor("_Color", Color.white);
    }

    //This method is called by regular bullets
    public void GotHit(int damage)
    {
        health -= damage;
    }

    //This method is called by poisoned bullets
    public void GotHit(int damage, int damageOverTime)
    {
        health -= damage;
        isPoisoned = true;
        //Turns the skeleton Green to show it's poisoned
        skeletonMaterial.SetColor("_Color", Color.green);
        appliedDamageOverTime = damageOverTime;
    }

    //This method is called by stun bullets
    public void GotHit(int damage, float stunTime)
    {
        health -= damage;
        StartCoroutine(ApplyStun(stunTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Turret")
        {
            collision.gameObject.GetComponent<Turret>().GotHit(meleeDamage);
            Destroy(this.gameObject);
        }
    }

}
