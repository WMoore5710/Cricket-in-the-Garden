using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CentipedeController : MonoBehaviour
{  
    [Header("Points")]
        public int maxHeight;
        public int minHeight;
    [Header("radiation vars")]
        public GameObject LowRad;
        public GameObject MidRad;
        public GameObject HighRad;
        public LayerMask playerLayer;
        public float maxRadDist;
        public float medRadDist;
        public float lowRadDist;
    [Header("movement")]
        public int MaxSpeed;
        public float waitTime;
        public float MoveSpeed;
    [Header("refrences")]
        public static CentipedeController ScriptRef;
        public Rigidbody2D rb;
        private bool movingUp;
        private bool isWaiting;
    // Start is called before the first frame update
    void Start() {
        /*isAtMaxOrMin = false;
        ScriptRef = this;
        rb = ScriptRef.GetComponent<Rigidbody2D>();
        StartCoroutine(move(true));
        currentDirection = true;
        corouStopper = false;
        */
        rb = GetComponent<Rigidbody2D>();
        movingUp = true;
        isWaiting = false;
        StartCoroutine(OscillateMovement());
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().takeDamage(100);
        }
    }
    // Update is called once per frame
    void Update() {
        /*if (((rb.position.y >= maxHeight) && !isAtMaxOrMin && !corouStopper) || ((rb.position.y <= minHeight) && !isAtMaxOrMin && !corouStopper)) {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            corouStopper = true;
            Debug.Log("max height reached");
            isAtMaxOrMin = true;
            rb.velocity = new Vector3(0, 0, 0);
            StartCoroutine(waitAndFlip(currentDirection));
        }*/
    }
    // method was origonally written by me and has been adapted by AI to work better Claude.AI https://claude.ai/chat/511f3e41-1716-481b-81c1-90ae90fb307c
    private IEnumerator OscillateMovement()
    {
        while (true)
        {
            // Check if we need to change direction
            if (!isWaiting)
            {
                if (movingUp && rb.position.y >= maxHeight)
                {
                    // Reached top point
                    yield return StartCoroutine(WaitAndChangeDirection());
                }
                else if (!movingUp && rb.position.y <= minHeight)
                {
                    // Reached bottom point
                    yield return StartCoroutine(WaitAndChangeDirection());
                }
                else
                {
                    // Apply force in current direction
                    Vector2 forceDirection = movingUp ? Vector2.up : Vector2.down;
                   
                    // Only apply force if below max speed
                    if (Mathf.Abs(rb.velocity.y) < MaxSpeed)
                    {
                        rb.AddForce(forceDirection * MoveSpeed);
                    }
                   
                    Debug.Log("Moving " + (movingUp ? "up" : "down") + ", velocity: " + rb.velocity.y);
                }
            }
           
            yield return new WaitForFixedUpdate();
        }
    }
   
    private IEnumerator WaitAndChangeDirection()
    {
        // Stop current movement
        isWaiting = true;
        rb.velocity = Vector2.zero;
       
        Debug.Log("Reached " + (movingUp ? "top" : "bottom") + ", waiting...");
       
        // Wait specified time
        yield return new WaitForSeconds(waitTime);
       
        // Change direction
        movingUp = !movingUp;
        isWaiting = false;
       
        Debug.Log("Changing direction to " + (movingUp ? "up" : "down"));
    }
}