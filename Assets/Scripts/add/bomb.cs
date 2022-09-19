using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    Animator anim;
    public aiControler aiCont;
    public CarControler cont;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            if (other.gameObject.transform.parent.gameObject.name != this.gameObject.transform.parent.gameObject.name)
            {
                if (other.gameObject.transform.parent.gameObject.tag == "Player" && other.gameObject.name != "bomb")
                {
                    Debug.Log("player bomb");
                    cont = other.gameObject.transform.parent.gameObject.GetComponent<CarControler>();
                    cont.bombHit = true;
                }
                else if (other.gameObject.transform.parent.gameObject.tag == "AiCar" && other.gameObject.name != "bomb" && other.gameObject.name != "bombFire")
                {
                    Debug.Log("ai bomb");
                    aiCont = other.gameObject.transform.parent.gameObject.GetComponent<aiControler>();
                    aiCont.bombHit = true;
                }


            }
        }
    }

    public void bombExplore()
    {
        
        anim.SetBool("bomb", true);
        Debug.Log("work");
        StartCoroutine(close());
        
    }

    IEnumerator close()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(close2());
    }

    IEnumerator close2()
    {
        yield return new WaitForFixedUpdate();
        
        StartCoroutine(close3());
    }

    IEnumerator close3()
    {

        yield return new WaitForEndOfFrame();
        if (anim.GetBool("bomb"))
        {
            anim.SetBool("bomb", false);
        }
    }
}
