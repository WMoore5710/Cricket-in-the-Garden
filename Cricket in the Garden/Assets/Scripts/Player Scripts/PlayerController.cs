using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Player Controller script. Contains most player functions.
Weapon Controller Handles the players gun functions. That script is put on the rotation point that is a child of this player object. 
*/
public class PlayerController : MonoBehaviour
{
    [Header("Player stuff")]
        public Rigidbody2D rb;
        public int health;

    [Header("Movement Variables")]
        private bool facingRight;
        private bool callJump;
        private float xMovement;
        public float jumpForce;
        public float speed;
    
    [Header("Rand vars")]
        public LayerMask surface;

    [Header("Sneak vars")] 
        public GameObject sneakScreen;
        public bool isSneaking;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isSneaking = false;
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
        Vector2 position = new(transform.position.x, transform.position.y-(transform.localScale.y) + 2.0f);
        float distance = 0.5f;
        // adds a circle at the feet of the player that is used to check if the player is touching the ground or not
        return Physics2D.OverlapCircle(position, distance, surface);
    }
    public void sneakToggle(bool toggle) {
        sneakScreen.SetActive(toggle);
        if (toggle) {
            isSneaking = true;
        } else {
            isSneaking = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("bush")) {
            sneakToggle(true);
        }
        if (collision.CompareTag("bushOut") && isSneaking) {
            sneakToggle(false);
        }
    }
}
