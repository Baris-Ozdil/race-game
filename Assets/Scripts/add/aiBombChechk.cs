using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiBombChechk : MonoBehaviour
{

    public aiControler aiCont;

    // Start is called before the first frame update
    void Start()
    {
        aiCont = this.gameObject.transform.parent.gameObject.GetComponent<aiControler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            if (other.gameObject.transform.parent.gameObject.name != this.gameObject.transform.parent.gameObject.name && (other.gameObject.transform.parent.gameObject.tag == "Player" || other.gameObject.transform.parent.gameObject.tag == "AiCar"))
            {

                aiCont.fire = true;

            }
        }
    }
}
