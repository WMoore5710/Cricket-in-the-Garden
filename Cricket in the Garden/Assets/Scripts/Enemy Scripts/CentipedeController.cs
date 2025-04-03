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
    [Header("movement")]
        public int MaxSpeed;
        private bool isAtMaxOrMin;
        public float waitTime;
        public bool currentDirection;
        public float MoveSpeed;
    [Header("refrences")]
        public static CentipedeController ScriptRef;
        public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start() {
        ScriptRef = this;
        rb = ScriptRef.GetComponent<Rigidbody2D>();
        StartCoroutine(move(true));
        currentDirection = true;
    }

    // Update is called once per frame
    void Update() {
        if (rb.position.y >= maxHeight || rb.position.y <= minHeight && !isAtMaxOrMin) {
            isAtMaxOrMin = true;
            rb.velocity = new Vector3(0, 0, 0);
            StartCoroutine(waitAndFlip(currentDirection));
        }
    }
    // method waits after max or min height has been hit and then flips the movement and calls move again
    private IEnumerator waitAndFlip(bool oldDirection) {
        yield return new WaitForSeconds(waitTime);
        currentDirection = !oldDirection;
        StartCoroutine(move(currentDirection));
    }
    private IEnumerator move(bool upOrDown) { // true is up. false is down\
        Debug.Log("moveRan");
        Debug.Log(upOrDown);
        while (!isAtMaxOrMin) {
            if (upOrDown) {
                if (Mathf.Abs(rb.velocity.y) < MaxSpeed) {           // move upwards
                    rb.AddRelativeForce (Vector3.up * MoveSpeed);
                    WaitForSeconds wait = new WaitForSeconds(0.1f);
                    yield return wait;
                }
            } else if (!upOrDown) {
                if (Mathf.Abs(rb.velocity.y) < MaxSpeed) {          // move downwards
                    rb.AddRelativeForce (Vector3.down * MoveSpeed);
                    WaitForSeconds wait = new WaitForSeconds(0.1f);
                    yield return wait;
                }
            }
        }
    }
}
