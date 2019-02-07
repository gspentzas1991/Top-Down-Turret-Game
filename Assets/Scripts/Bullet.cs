using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The bullet moves forward until it hits an enemy. When it does it calls a Skeleton method to inform him of the hit and destroys itself
 * If the bullet doesn't find an enemy, it destroys itself after one second
 */
public class Bullet : MonoBehaviour {

    public int speed;
    public int damage;
    
    private void Start()
    {
        StartCoroutine(DestroyAfterSeconds(1));
    }

    void Update ()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    //This method can be overridden in order to have different behaviours on hit
    public virtual void OnTriggerEnter(Collider target)
    {
        if(target.gameObject.tag=="Enemy")
        {
            Skeleton targetSkeleton = target.gameObject.GetComponent<Skeleton>();
            targetSkeleton.GotHit(damage);
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
