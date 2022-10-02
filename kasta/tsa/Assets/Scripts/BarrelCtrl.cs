using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expBarrel;

    private Transform tr;
    private Rigidbody rb;

    private int hitcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            if (++hitcount == 3)
            {
                ExpBarrel();
            }
        }
    }
    void ExpBarrel()
    {
        GameObject exp = Instantiate(expBarrel, tr.position, Quaternion.identity);
        Destroy(exp, 5.0f);

        rb.mass = 1.0f;
        rb.AddForce(Vector3.up * 1500.0f);

        Destroy(gameObject, 3.0f);
    }
}