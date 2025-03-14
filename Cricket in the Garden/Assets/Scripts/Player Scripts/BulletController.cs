using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    public int bulletDMG;
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("enemy")){
            EnemyController enemyInstance = collision.GetComponent<EnemyController>();
            if (enemyInstance != null) {
                enemyInstance.takeDamage(bulletDMG);
            }
            Destroy(gameObject);
        }
    }
}
