using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerController : MonoBehaviour
{
    
   private float speed = 50f;
private float timeToDestroy = 3f;
public Vector3 target { get; set; }
public bool hit{ get; set; }

    private void OnEnable(){
        Destroy(gameObject, timeToDestroy);

    }

    // Update is called once per frame
    void Update()
    {
         transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
         if(!hit && Vector3.Distance(transform.position, target) < .01f) {
            Destroy(gameObject);
         }
    }
    private void OnCollisionEnter(Collision other) {
        
        Destroy(gameObject);
    }
}
