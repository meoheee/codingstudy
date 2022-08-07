using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   float h = input.GetAxis("Horizontal");
        float v = input.GetAxis("Vertical");
        
        Debug.log("h =" + h);
        Debug.log("v =" + v);

        transform.position = new Vector3(0, 0, 1);
    }
}
ss