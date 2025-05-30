using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
This script manages the basic functions of the Crow enemys. 
Including FOV/Player Detection, Movement, attacks etc...
*/
public class EnemyController : MonoBehaviour
{
    [Header("Instance")]
        public static EnemyController enemyInstance;
        public GameObject firePoint;
    [Header("GamePlay vars")]
        public int health;
        public int enemyDeathTimer;
    [Header("Player detection vars")]
        public float radius = 10.0f;
        [Range(1,360)] public float angle = 45f;
        public LayerMask targetLayer; // the layer for object that will be targeted by enemy in FOV check
        public LayerMask obstructLayer; // layer for objects that obstruct FOV of enemy
        public bool CanSeePlayer {get; private set;}
        public PlayerController playerRef;
    [Header("RigidBody Vars")]
        public float flySpeed;
        private Rigidbody2D rb;
        public int crowMaxSpeed;
    [Header("Attack Vars")]
        public float attRandDelay;
        public GameObject lazer;
        bool lazerActive = false;
    // Start is called before the first frame update
    void Start()
    {   
        enemyInstance = this;
        rb = enemyInstance.GetComponent<Rigidbody2D>();
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(FOVCheck());
        StartCoroutine(fly());
        Destroy(gameObject, enemyDeathTimer);
    }

    // Update is called once per frame
    void Update() {
        if (CanSeePlayer && !lazerActive) {
            Debug.Log("lazer coroutine");
            StartCoroutine(AttackPlayer());
        }
    }
    public void lazerAtt() {
        Instantiate(lazer, firePoint.transform.position, firePoint.transform.rotation);
    }
    private IEnumerator FOVCheck() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while(true) {
            yield return wait;
            FOV();
        }
    }
    void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }
    // crow velocity adder.
    private IEnumerator fly() {
        while (true) {
            if (rb.velocity.x < crowMaxSpeed) {
                rb.AddRelativeForce (Vector3.left * flySpeed);
                WaitForSeconds wait = new WaitForSeconds(0.1f);
                yield return wait;
            }
        }
    }
    // Attack player runs while the player is in the crows sight. It creates a random chance for the crow to attack while the player is in that range.
    private IEnumerator AttackPlayer() {
        lazerActive = true;
        int AttackRand = Random.Range(1,2);
        yield return new WaitForSeconds(attRandDelay);
        if (AttackRand == 1) {
            lazerAtt();
            yield return new WaitForSeconds(2);
            lazerActive = false;
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
                if (!Physics2D.Raycast(transform.position, directionToTarget,distanceToTarget, obstructLayer) && !playerRef.isSneaking) {
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
    // method used outside of this script for this entity to take damage.
    public void takeDamage(int amount){
            health -= amount;
            if(health <= 0){
            Destroy(gameObject);
        }
    }
}