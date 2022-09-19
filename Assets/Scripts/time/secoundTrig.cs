using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secoundTrig : MonoBehaviour
{
    public lapTime lapTimeSicript;


    private void OnTriggerEnter(Collider other)
    {
        lapTimeSicript = other.gameObject.transform.parent.gameObject.GetComponent<lapTime>();

        if (other.gameObject.transform.parent.gameObject.tag == "Player" && lapTimeSicript.firstTrig == true && lapTimeSicript.secoundTrig == false)
        {

            lapTimeSicript.secoundTrig = true;

        }
        else if (other.gameObject.transform.parent.gameObject.tag == "AiCar" && lapTimeSicript.firstTrig == true && lapTimeSicript.secoundTrig == false)
        {

            lapTimeSicript.secoundTrig = true;

        }
    }
}
