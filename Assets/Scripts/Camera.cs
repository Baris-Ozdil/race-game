using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public GameObject CamP;
    public GameObject lookP;
    public GameObject player;
    private float camSpeed = 20;
    private float camSpeed2 = 0 ;
    public float defaultFov, changeFoc = 125 , smothTime = 5; 

    // Start is called before the first frame update
    void Start()
    {
        CamP = GameObject.FindGameObjectWithTag("CameraP");
        lookP = GameObject.FindGameObjectWithTag("LookP");
        player = GameObject.FindGameObjectWithTag("Player");
        var carCont = player.GetComponent<CarControler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var carCont = player.GetComponent<CarControler>();

        camSpeed2 = (float)carCont.Kph;
        camSpeed = Mathf.Lerp(camSpeed, camSpeed2 /4 , Time.deltaTime);

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, CamP.transform.position, Time.deltaTime * camSpeed );
        gameObject.transform.LookAt(lookP.transform.position);

    }
}
