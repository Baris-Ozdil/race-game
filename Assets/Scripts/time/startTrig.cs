using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTrig : MonoBehaviour
{

    public lapTime lapTimeSicript;

    private void OnTriggerEnter(Collider other)
    {
        lapTimeSicript = other.gameObject.transform.parent.gameObject.GetComponent<lapTime>();

        if (other.gameObject.transform.parent.gameObject.tag == "Player" && lapTimeSicript.firstTrig == true && lapTimeSicript.secoundTrig == true)
        {

            lapTimeSicript.startTig = true;
            lapTimeSicript.lapFinish();

        }
        else if (other.gameObject.transform.parent.gameObject.tag == "AiCar" && lapTimeSicript.firstTrig == true && lapTimeSicript.secoundTrig == true)
        {

            lapTimeSicript.startTig = true;
            lapTimeSicript.lapFinish();

        }
    }
}
