using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletremove : MonoBehaviour
{
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("bullet")) ;
        {
            Destroy(coll.gameObject);
        }
    }
}