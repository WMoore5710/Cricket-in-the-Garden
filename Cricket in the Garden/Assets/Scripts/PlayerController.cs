using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player stuff")]
    public Rigidbody2D rb;

    [Header("Movement Variables")]
    private bool facingRight;
    private bool callJump;
    private float xMovement;
    public float jumpForce;
    public float speed;
    
    [Header("Rand vars")]
    public LayerMask surface;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xMovement = Input.GetAxis("Horizontal")*speed;
        if (isOnSurface() && Input.GetButtonDown("Jump")) {
            callJump = true;
        }
    }
    void FixedUpdate() {
        if (xMovement != 0) {
            movePlayer();
        }
        if (callJump) {
            jump();
        }
    }
    private void movePlayer() {
        Vector2 movement = new Vector2(xMovement, rb.velocity.y);
        rb.velocity = movement;
        //if (xMovement > 0) {
        //    facingRight = xMovement > 0;
        //}
    }
    private void jump() {
        callJump = false;
        rb.AddForce(transform.up*jumpForce, ForceMode2D.Impulse);
    }
    // method taken from my semester 1 project cuz its complicated and i dont fully understand it
    private bool isOnSurface() {
        Vector2 position = new(transform.position.x, transform.position.y-(transform.localScale.y));
        float distance = 0.2f;
        // adds a circle at the feet of the player that is used to check if the player is touching the ground or not
        return Physics2D.OverlapCircle(position, distance, surface);
    }
}
