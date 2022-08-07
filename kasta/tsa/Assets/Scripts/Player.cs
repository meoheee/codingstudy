using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float X = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && X == 1) || (isTouchLeft && X == -1))
            X = 0;

        float Y = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && Y == 1) || (isTouchBottom && Y == -1))
            Y = 0;
        transform.Translate(new Vector3(X, Y, 0) * Time.deltaTime * Speed);
    }
    
}
