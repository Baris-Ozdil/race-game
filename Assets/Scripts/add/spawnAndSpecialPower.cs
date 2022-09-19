using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnAndSpecialPower : MonoBehaviour
{
    public GameObject spawnObje;
    public int chouse;
    public CarControler cont;
    public aiControler aiCont;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cont = other.gameObject.GetComponentInParent<CarControler>();
           if (!cont.specialPowerFlag)
            {
                switch (chouse)
                {

                    case 0:
                        cont.speedBosst = true;
                        Debug.Log(chouse);
                        break;
                    case 1:
                        cont.breakBosst = true;
                        Debug.Log(chouse);
                        break;
                    case 2:
                        cont.bomb = true;
                        Debug.Log(chouse);
                        break;

                }
                spawnAndDestroy();
            }
        }
        else if(other.gameObject.tag == "AiCar")
        {
            aiCont = other.gameObject.GetComponentInParent<aiControler>();

            if (!aiCont.specialPowerFlag)
            {
                
                switch (chouse)
                {
                    case 0:
                        aiCont.speedBosst = true;
                        Debug.Log(chouse);
                        break;
                    case 1:
                        aiCont.breakBosst = true;
                        Debug.Log(chouse);
                        break;
                    case 2:
                        aiCont.bomb = true;
                        Debug.Log(chouse);
                        break;

                }
                spawnAndDestroy();
            }
        }
    }

    private void spawnAndDestroy()
    {
        Instantiate(spawnObje, gameObject.transform.position, gameObject.transform.rotation);

        Destroy(gameObject);

    }
}
