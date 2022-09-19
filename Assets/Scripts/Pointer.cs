using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    public GameObject pointer;
    public GameObject AiPoint_1;
    public GameObject AiPoint_2;
    public GameObject AiPoint_3;
    public GameObject AiPoint_4;
    public GameObject AiPoint_5;
    public GameObject AiPoint_6;
    public GameObject AiPoint_7;
    public GameObject AiPoint_8;
    public GameObject AiPoint_9;
    public GameObject AiPoint_10;
    public GameObject AiPoint_11;
    public GameObject AiPoint_12;
    public float pointTrigger = 1;


    // Update is called once per frame
    void Update()
    {
        if(pointTrigger == 1)
        {
            pointer.transform.position = AiPoint_1.transform.position;
        }
        else if (pointTrigger == 2)
        {
            pointer.transform.position = AiPoint_2.transform.position;
        }
        else if (pointTrigger == 3)
        {
            pointer.transform.position = AiPoint_3.transform.position;
        }
        else if (pointTrigger == 4)
        {
            pointer.transform.position = AiPoint_4.transform.position;
        }
        else if (pointTrigger == 5)
        {
            pointer.transform.position = AiPoint_5.transform.position;
        }
        else if (pointTrigger == 6)
        {
            pointer.transform.position = AiPoint_6.transform.position;
        }
        else if (pointTrigger == 7)
        {
            pointer.transform.position = AiPoint_7.transform.position;
        }
        else if (pointTrigger == 8)
        {
            pointer.transform.position = AiPoint_8.transform.position;
        }
        else if (pointTrigger == 9)
        {
            pointer.transform.position = AiPoint_9.transform.position;
        }
        else if (pointTrigger == 10)
        {
            pointer.transform.position = AiPoint_10.transform.position;
        }
        else if (pointTrigger == 11)
        {
            pointer.transform.position = AiPoint_11.transform.position;
        }
        else if (pointTrigger == 12)
        {
            pointer.transform.position = AiPoint_12.transform.position;
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "AiCar")
        {
            this.GetComponent<BoxCollider>().enabled = false;
            pointTrigger++;
            if (pointTrigger == 13)
            {
                pointTrigger = 1;
            }
        }

        yield return new WaitForSeconds(0.1f);

        this.GetComponent<BoxCollider>().enabled = true;
    }

}
