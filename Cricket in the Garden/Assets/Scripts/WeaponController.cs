using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
This script manages the instatiation of bullets at the firepoint of the Player. 
Bullet logic such as velocity and rotation is handled (going to be) Within the bullet script that will go on the actual bullet Pre fab Object.
*/
public class WeaponController : MonoBehaviour
{   
    [Header("Unity Objects")]
        public Transform firePoint;
        public GameObject bulletPreFab;
    [Header("Timer Vars")]
        public float FireDelay;
        private bool canFire;
        private bool timerOn;
        public int timeBetweenFiring;
    [Header("Misc Vars")]
        private Vector3 mousePos;
        private Camera mainCam;
        public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Logic taken from my semester project. The firing works mostly the same
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // find position of mouse

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ);
        
        // if you can fire and click to fire
        if (Input.GetMouseButton(0) && canFire && !player.GetComponent<PlayerController>().isSneaking) {
            canFire = false;
            Instantiate(bulletPreFab, firePoint.position, Quaternion.identity); // functions of the bullet created here are manged in BulletController on the prefabs
        }
        // if you cant fire then timer is started
        if (!canFire && !timerOn) {
            StartCoroutine(timer());
        }
    }
    IEnumerator timer() {
        timerOn = true;
        yield return new WaitForSeconds(timeBetweenFiring);
        canFire = true;
        timerOn = false;
    }
}
