using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerDmg : MonoBehaviour
{      
    public PlayerController playerRef;
    public int damage;
    public float Lazertime;
    public GameObject parentRef;
    // Start is called before the first frame update
    void Start() {
        parentRef = this.transform.parent.gameObject;
        StartCoroutine(deathCountdown());
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private IEnumerator deathCountdown() {
        yield return new WaitForSeconds(Lazertime);
        Destroy(parentRef);
    }
}
