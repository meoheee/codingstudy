using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletremove : MonoBehaviour
{
    public GameObject sparkEffect;
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("bullet")) ;
        {
            ContactPoint cp = coll.GetContact(0);
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            Destroy(spark, 0.5f);
            Destroy(coll.gameObject);
        }
    }
}