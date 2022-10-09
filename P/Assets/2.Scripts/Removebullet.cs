using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removebullet : MonoBehaviour
{
    public GameObject sparkEffect;
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("bullet"))
        {
            ContactPoint cp = coll.GetContact(0);
            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            GameObject spart = Instantiate(sparkEffect, cp.point, rot);
            Destroy(spart, 0.5f);
            Destroy(coll.gameObject);
        }
    }
}
