using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRubble : MonoBehaviour
{
Vector3 originalPos;

void Awake()
{
	originalPos = gameObject.transform.position;
}

void OnCollisionEnter(Collision collision)
 {
     if(collision.gameObject.tag == "bottom")
     {
         gameObject.transform.position = originalPos;
     }

 }

}