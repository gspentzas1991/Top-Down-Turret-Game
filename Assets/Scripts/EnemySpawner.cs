using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spawns the enemies at set intervals, outside of the camera's vision, and passes to them the turret GameObject. The random positions the enemies can appear in are: 
 * X axis between -12 to -7 and 7 to 12, Y axis between -5 to -10 and 5 to 10
 */
public class EnemySpawner : MonoBehaviour {

    public float spawningSpeed;
    public GameObject turret;
    public GameObject skeleton;

    // Use this for initialization
    private void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
       GameObject newEnemy= Instantiate(skeleton, new Vector3((Random.Range(0,2)*2-1) * Random.Range(7, 12), 1, (Random.Range(0, 2) * 2 - 1) * Random.Range(5, 10)),transform.rotation);
        newEnemy.GetComponent<Skeleton>().turret = turret;
        StartCoroutine(waitBeforeSpawning(spawningSpeed));
    }

    IEnumerator waitBeforeSpawning(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SpawnEnemy();
    }
}
