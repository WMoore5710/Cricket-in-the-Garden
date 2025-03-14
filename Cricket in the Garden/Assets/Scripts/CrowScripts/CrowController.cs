using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    [Header("Obj Refrences")]
        public GameObject crow;
        public Transform crowSpawn;
    [Header("rand")]
        bool onCrowCooldown;
        public int crowSpawnDelay;
    // Start is called before the first frame update
    void Start()
    {
        onCrowCooldown = false;
        StartCoroutine(CreateCrows());
    }
    private IEnumerator CreateCrows() {
        while (true) {
            int crowrand = Random.Range(0, 5);
            if (crowrand == 1 && !onCrowCooldown) {
                Instantiate(crow, crowSpawn.position, Quaternion.identity);
                onCrowCooldown = true;
            }
            if (onCrowCooldown) {
                yield return new WaitForSeconds(crowSpawnDelay);
                onCrowCooldown = false;
            }
        }    
    }
}
