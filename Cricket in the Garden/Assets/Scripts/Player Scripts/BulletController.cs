using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    public int bulletDMG;
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("enemy")){
            var healthComponent = collision.GetComponent<EnemyController>();
            if (healthComponent != null)
            {
                healthComponent.takeDamage(bulletDMG);
            }
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("enemy")){
            Destroy(gameObject);
        }
    }
}
