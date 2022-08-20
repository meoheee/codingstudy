using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform tr;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float h =Input.GetAxis("Horizontal");
        float v =Input.GetAxis("Vertical");

        Debug.Log("h = " + h);
        Debug.Log("v = " + v);

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir.normalized * moveSpeed*Time.deltaTime);
    }
    
}
