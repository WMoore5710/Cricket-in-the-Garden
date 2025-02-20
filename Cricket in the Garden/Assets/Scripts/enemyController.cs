using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
This script manages (going to manage) Basic functions of the player enemys. 
Including FOV/Player Detection, Movement (not yet), etc...
*/
public class EnemyController : MonoBehaviour
{
    [Header("Instance")]
        public static EnemyController enemyInstance;
    [Header("GamePlay vars")]
        public int health;
    [Header("Player detection vars")]
        public float radius = 5.0f;
        [Range(1,360)] public float angle = 45f;
        public LayerMask targetLayer; // the layer for object that will be targeted by enemy in FOV check
        public LayerMask obstructLayer; // layer for objects that obstruct FOV of enemy
        public bool CanSeePlayer {get; private set;}
        public GameObject playerRef;
    // Start is called before the first frame update
    void Start()
    {   
        playerRef = GameObject.FindGameObjectWithTag("Player");
        enemyInstance = this;
        StartCoroutine(FOVCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FOVCheck() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while(true) {
            yield return wait;
            FOV();
        }
    }
    // FOV method creates a circle overlap and then uses a layermask to see if the anything in target layer (the player) is inside of that circle overlap
    private void FOV() {
        // creates circle overlap here
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        if(rangeCheck.Length > 0) {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            if(Vector2.Angle(transform.up, directionToTarget)< angle/2) {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);
                // check to see if player is not obstructed by walls (anything in the obstruct layer)
                if (!Physics2D.Raycast(transform.position, directionToTarget,distanceToTarget, obstructLayer)) {
                    // if player is not behind walls
                    CanSeePlayer = true;
                } else {
                    // if player is behind walls
                    CanSeePlayer = false;
                }
            }
            else {
                CanSeePlayer = false;
            }
        }
        else if(CanSeePlayer) {
            // if player is not the circle overlap then set can see player to false
            CanSeePlayer = false;
        }
    }
}