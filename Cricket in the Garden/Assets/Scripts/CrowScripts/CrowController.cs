using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    [Header("Obj Refrences")]
        public GameObject crow;
        public Transform crowSpawn;
    [Header("rand")]
        bool hasCrowSpawned;
    // Start is called before the first frame update
    void Start()
    {
        hasCrowSpawned = false;
    }

    // Update is called once per frame
    // Fixed update is used here for controlling crow RNG
    void FixedUpdate() { 
        int crowrand = Random.Range(0, 15);
        if (crowrand == 1 && !hasCrowSpawned) {
            Instantiate(crow, crowSpawn.position, Quaternion.identity);
            hasCrowSpawned = true;
        }
    }
}
