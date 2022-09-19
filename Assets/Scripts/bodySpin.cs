using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodySpin : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (anim.GetBool("spin"))
        {
            anim.SetBool("spin", false);
        }
    }

    public void spinCar()
    {

        anim.SetBool("spin", true);
        Debug.Log("work");

    }

}
