using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvarsManager : MonoBehaviour
{
    public CarControler cont;
    public lapTime lapTimeSicript;

    public GameObject player;
    public GameObject needle;
    public Text kph;
    public Text gear;
    public Text lapTime;
    public Text bestTime;
    public Text lapCount;
    public Text totalLab;
    public Text remainTime;
    public Text typ;
    public Slider nitroSlider;
    int speed;
    private float needlePOs;
    private float needleStart = 19.419001f ;
    private float needleFinish = -190f ;
    float need;
    public float nitroSize = 25;
    private int bestMin = 9999;
    private int bestSec = 99;
    private float besMil = 101 ;
    private string min;
    private string sec;
    private string milSec;
    private string bestmin;
    private string bestsec;
    private string bestmilSec;
    private int lap;
    private int totLap;



    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        cont = player.GetComponent<CarControler>();
        lapTimeSicript = player.GetComponent<lapTime>();
    }

    public void lapAndTimeUpdate()
    {
        if(bestMin > lapTimeSicript.minCount)
        {
            bestMin = lapTimeSicript.minCount;
            bestSec = lapTimeSicript.secCount;
            besMil = lapTimeSicript.milSecCont;
        }
        else if( bestMin < lapTimeSicript.minCount)
        {
            return;
        }
        else if (bestMin == lapTimeSicript.minCount)
        {

            if(bestSec < lapTimeSicript.secCount)
            {
                return;
            }
            else if(bestSec > lapTimeSicript.secCount)
            {
                bestMin = lapTimeSicript.minCount;
                bestSec = lapTimeSicript.secCount;
                besMil = lapTimeSicript.milSecCont;
            }
            else if(bestSec == lapTimeSicript.secCount)
            {

                if(besMil < lapTimeSicript.milSecCont)
                {
                    return;
                }
                else if( besMil > lapTimeSicript.milSecCont )
                {
                    bestMin = lapTimeSicript.minCount;
                    bestSec = lapTimeSicript.secCount;
                    besMil = lapTimeSicript.milSecCont;
                }

            }


        }

        bestTimeWrite();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!cont.reverse)
        {
            gear.text = (cont.gearNum+1).ToString();
        }
        else
        {
            gear.text = "R";
        }
        

        speed = (int)cont.Kph;
        kph.text = speed.ToString("0");
        remainTime.text = "Remaining Time: " + cont.remainingTime ;
        typ.text = cont.typ;
        updateNeedle();
        nitroSLiderUpdate();

        lapCount.text = lapTimeSicript.lapCount.ToString() + " / " + lapTimeSicript.totLap.ToString() + "LAP";

        lapTimeWrite();
        bestTimeWrite();
    }


    public void lapTimeWrite()
    {
        min = lapTimeSicript.minCount.ToString();
        sec = lapTimeSicript.secCount.ToString();
        milSec = lapTimeSicript.milSecCont.ToString("F0");
        if(lapTimeSicript.secCount <= 9)
        {
            lapTime.text = "Lap Time: " + min + ".0" + sec + "." + milSec;
        }
        else
        {
            lapTime.text = "Lap Time: " + min + "." + sec + "." + milSec;
        }
        

    }

    public void bestTimeWrite()
    {
        if (lapTimeSicript.lapComplete)
        {
            bestmin = bestMin.ToString();
            bestsec = bestSec.ToString();
            bestmilSec = besMil.ToString("F0");
            if(bestSec <= 9)
            {
                bestTime.text = "Best Time: " + bestmin + ".0" + bestsec + "." + bestmilSec;
            }
            else
            {
                bestTime.text = "Best Time: " + bestmin + "." + bestsec + "." + bestmilSec;
            }
            
        }
        
    }
    public void updateNeedle()
    {
        needlePOs = needleStart - needleFinish;
        need = cont.enginRPM / 10000;
        needle.transform.eulerAngles = new Vector3 (0, 0, (needleStart - need * needlePOs));

    }

    public void nitroSLiderUpdate()
    {
        nitroSlider.value = cont.nitroValue / nitroSize;
    }

}
