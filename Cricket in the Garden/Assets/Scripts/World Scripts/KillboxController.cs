using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillboxController : MonoBehaviour
{
    public int damage;
private void OnCollisionEnter2D(Collision2D collision){
    if (collision.collider.CompareTag("Player")){
        if(collision.gameObject.TryGetComponent(out PlayerController playerController)){
            playerController.takeDamage(damage);
        }
    }
}
}
