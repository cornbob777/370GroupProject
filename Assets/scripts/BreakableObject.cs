using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
   void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag ("lazer"))
        {
            Destroy(gameObject);
    }
}
}