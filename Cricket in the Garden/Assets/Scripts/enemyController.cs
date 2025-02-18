using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [Header("Instance")]
    public static enemyController enemyInstance;
    // Start is called before the first frame update
    void Start()
    {
        enemyInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
