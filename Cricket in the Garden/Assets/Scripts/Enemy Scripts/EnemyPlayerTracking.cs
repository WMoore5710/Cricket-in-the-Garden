using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerTracking : MonoBehaviour
{
    public Transform target;
    public Vector3 targetPos;
    public Vector3 thisPos;
    public float angle;
        
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
        
    void LateUpdate() {
        // tracking player script found from this forum: https://discussions.unity.com/t/transform-lookat-target-in-2d/105326/3 
        targetPos = target.position;
        thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180f));
    }
}
