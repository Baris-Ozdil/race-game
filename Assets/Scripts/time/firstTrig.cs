using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstTrig : MonoBehaviour
{
    public lapTime lapTimeSicript;

    private void OnTriggerEnter(Collider other)
    {
        lapTimeSicript = other.gameObject.transform.parent.gameObject.GetComponent<lapTime>();

        if (other.gameObject.transform.parent.gameObject.tag == "Player"  && lapTimeSicript.firstTrig == false && lapTimeSicript.secoundTrig == false)
        {
            
            lapTimeSicript.firstTrig = true;

        }
        else if(other.gameObject.transform.parent.gameObject.tag == "AiCar"  && lapTimeSicript.firstTrig == false && lapTimeSicript.secoundTrig == false )
        {

            lapTimeSicript.firstTrig = true;

        }
        else if (other.gameObject.transform.parent.gameObject.tag == "Player" && lapTimeSicript.firstTrig == true && lapTimeSicript.secoundTrig == true)
        {

            lapTimeSicript.firstTrig = false;
            lapTimeSicript.secoundTrig = false;
            

        }
        else if (other.gameObject.transform.parent.gameObject.tag == "AiCar" && lapTimeSicript.startTig == true && lapTimeSicript.firstTrig == false)
        {

            lapTimeSicript.firstTrig = false;
            lapTimeSicript.secoundTrig = false;
            

        }
    }

}
