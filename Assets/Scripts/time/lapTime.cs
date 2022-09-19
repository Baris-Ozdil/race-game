using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lapTime : MonoBehaviour
{
    public canvarsManager canManager;

    public int minCount;
    public int secCount;
    public float milSecCont;
    public int lapCount = 1;
    public bool lapComplete = false; 
    public bool startTig = false;
    public bool firstTrig = false;
    public bool secoundTrig = false;
    public float realTime;
    public int totLap = 3;

    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.FindGameObjectWithTag("canvas");
        canManager = canvas.GetComponent<canvarsManager>();
    }

    private void FixedUpdate()
    {
        realTime = Time.time;
        milSecCont += Time.deltaTime * 10;

        if (milSecCont >= 10)
        {
            secCount++;
            milSecCont = 0;
        }

        if (secCount >= 60)
        {
            minCount++;
            secCount = 0;
        }
    }
    
    public void lapFinish()
    {
        if (startTig && firstTrig && secoundTrig)
        {
            lapComplete = true;
            lapCount++;
            startTig = false;
            firstTrig = false;
            secoundTrig = false;

            if (gameObject.tag == "Player"  )
            {
                canManager.lapAndTimeUpdate();
                if(lapCount > totLap)
                {
                    Cursor.lockState = CursorLockMode.None;
                    SceneManager.LoadScene(2);
                }
            }

            if(gameObject.tag == "AiCar" && lapCount > totLap)
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(3);
            }

            minCount = secCount = 0;
            milSecCont = 0;

        }
    }

}
