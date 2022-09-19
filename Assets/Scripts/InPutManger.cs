using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPutManger : MonoBehaviour
{

    public float vertical ;
    public float horizontal;
    public bool handBrake;
    public bool boost;




    // Update is called once per frame
    public void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        handBrake = (Input.GetAxis("Jump") != 0) ? true : false;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            boost = true;
        }
        else
        {
            boost = false;
        }

    }

}
