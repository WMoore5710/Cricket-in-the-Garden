using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
This script manages the instatiation of bullets at the firepoint of the Player. 
Bullet logic such as velocity and rotation is handled (going to be) Within the bullet script that will go on the actual bullet Pre fab Object.
*/
public class WeaponController : MonoBehaviour
{   
    // Start is called before the first frame update
    public Transform Gun;
    Vector2 direction;

    public GameObject Bullet;

    public float BulletSpeed;
    public Transform ShootPoint;
    public float fireRate;
    float ReadyForNextShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //Code taken and used by TheGameGuy for shooting. I did not use the recoil function later on in video. https://www.youtube.com/watch?v=NKF-FkDzE-s&t=532s
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)Gun.position;
        FaceMouse();

        if(Input.GetMouseButtonDown(0)){
            if(Time.time > ReadyForNextShot)
            {   
                ReadyForNextShot = Time.time + 1/fireRate;
                shoot();
            }
        }
    }

    void FaceMouse(){
        Gun.transform.right = direction;
    }

    void shoot(){
        GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * BulletSpeed);
        Destroy(BulletIns, 5);
    }
}

