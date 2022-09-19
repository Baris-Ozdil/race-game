using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiControler : MonoBehaviour
{
    internal enum tractionType
    {
        FW,
        RW,
        AW
    }

    [SerializeField] private tractionType tType;

    public WheelCollider[] Wheel = new WheelCollider[4];
    public GameObject[] meshes = new GameObject[4];
    public float[] fowardSdlip = new float[4];
    public float[] sideSdlip = new float[4];
    public aiInput aiIP;
    private GameObject WheelColliders, WhellMesh;
    public Rigidbody rigidbody;
    private GameObject centerOfMass;
    Vector3 WPFixer;
    public WheelFrictionCurve frictionFoward, frictionSide;
    public AnimationCurve mPower;
    public GameObject bomba;
    private bomb bombaScript;
    public GameObject body;
    private bodySpin spin;


    [Header("Variables")]
    public float torque = 1000;
    private float breakForce = -3000;
    public double Kph;
    public float KphH;
    private bool itCanBreak = false;
    private bool goBack = true;
    private bool goFoward = true;
    private float radius = 6;
    private float downForceConstant = 70;
    public int boostPower = 4600;
    private bool curveTimeCheck = true;
    private float velocity = 0;
    private float driftAmount;
    public float handBreakFriction = 2;
    public float frictionnMultiplier = 2;
    public bool smoke = false;
    public bool reloadBoost = false;
    private float wheelRPM = 0;
    public bool reverse = false;
    public float enginRPM = 9000, vertical;
    public float maxRPM = 8750;
    private float minRPM = 3000;
    private float lastEnginRpm;
    public bool test; 
    public float[] gears;
    public float[] gearChangeSpeed;
    public int gearNum = 0;
    public float nitroValue = 7;
    public bool nitroSmoke = false;
    public bool speedBosst = false;
    public bool speedBoosFlag = false;
    public bool breakBosst = false;
    public bool breakBoosFlag = false;
    public bool bomb = false;
    public bool bombHit = false;
    public bool specialPowerFlag = false;
    private bool wheelAnCheck = true;
    public bool bombHitflag = false;
    public bool fire = false;
    public bool track = false;

    // Start is called before the first frame update
    void Start()
    {

        getComponent();
        StartCoroutine(radiusUpdate());
        bombaScript = bomba.GetComponent<bomb>();
        spin = body.GetComponent<bodySpin>();

    }


    private void FixedUpdate()
    {
        wheelAnimation();
        move();
        steerCar();
        downForce();
        friction();
        driftAndMoreGrip();
        engineRPMcalculater();
        specialPowerManager();
        bombHitCar();

        lastEnginRpm = enginRPM;

        Kph = rigidbody.velocity.magnitude * 3.6;
        KphH = (float)Kph;
    }

    public void bombHitCar()
    {
        if (bombHit)
        {
            bombHitflag = true;
            StartCoroutine(timePastShorth());
            spin.spinCar();
            bombHit = false;
        }
        if (bombHitflag)
        {
            rigidbody.AddForce(-transform.forward * 15000);
        }

    }

    IEnumerator timePastShorth()
    {
        wheelAnCheck = false;
        yield return new WaitForSeconds(0.6f);
        bombHitflag = false;
        wheelAnCheck = true;
    }


    private void specialPowerManager()
    {
        if (speedBosst || breakBosst || bomb)
        {
            specialPowerFlag = true;
        }
        if (specialPowerFlag)
        {
            if (speedBosst)
            {
                speedBoosFlag = true;
                speedBosst = false;
                StartCoroutine(timePast());
            }
            else if (breakBosst)
            {
                breakBoosFlag = true;
                breakBosst = false;
                StartCoroutine(timePast());
            }
            else if (bomb)
            {
                if (fire)
                {
                    bomb = false;
                    bombaScript.bombExplore();
                    specialPowerFlag = false;
                    fire = false;
                }
            }



        }


    }

    IEnumerator timePast()
    {
        yield return new WaitForSeconds(15);
        speedBoosFlag = breakBoosFlag = false;
        specialPowerFlag = false;
    }


    public void engineRPMcalculater()
    {
        wheelsRPM();

        torque = mPower.Evaluate(enginRPM);

        float velocity = 0.0f;
        if (enginRPM >= maxRPM )
        {
            enginRPM = Mathf.SmoothDamp(enginRPM, maxRPM - 3500, ref velocity, 0.05f);
            test = (lastEnginRpm > enginRPM) ? true : false;
        }
        else if (enginRPM <= minRPM && gearNum != 0)
        {
            enginRPM = Mathf.SmoothDamp(enginRPM, maxRPM + 4500, ref velocity, 0.05f);
            test = (lastEnginRpm < enginRPM) ? true : false;
        }
        else
        {
            enginRPM = Mathf.SmoothDamp(enginRPM, 1000 + (Mathf.Abs(wheelRPM) * 15f - 3500 * (gears[gearNum] - 1)), ref velocity, 0.09f);
            test = false;
        }
        if (enginRPM >= maxRPM + 100) enginRPM = maxRPM + 100;

        gearShift();
    }

    public void wheelsRPM()
    {

        float sumWheelRPM = 0;

        for (int i = 0; i < 4; i++)
        {
            sumWheelRPM += Wheel[i].rpm;

        }

        wheelRPM = sumWheelRPM / 4;


        if (wheelRPM < 0 && !reverse)
        {
            reverse = true;
            
        }
        else if (wheelRPM > 0 && reverse)
        {
            reverse = false;
            
        }

    }

    private bool gearSpeedCheck()
    {
        if (Kph >= gearChangeSpeed[gearNum])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void gearShift()
    {

        if (!ground()) // if wheels are not ground don't change gear
        {
            return;
        }

        if (enginRPM > maxRPM && gearNum < gears.Length - 1 && !reverse && gearSpeedCheck())
        {
            gearNum++;
            return;
        }
        if (enginRPM < minRPM && gearNum > 0)
        {
            gearNum--;

        }

    }

    private bool ground() // lastikler yerdemi kontrol ediyor
    {
        if (Wheel[0].isGrounded && Wheel[1].isGrounded && Wheel[2].isGrounded && Wheel[3].isGrounded)
            return true;
        else
            return false;
    }

    IEnumerator radiusUpdate()
    {
        radius = 6 * KphH / 30;
        if (radius <= 3f && !aiIP.handBrake)
        {
            radius = 3f;
        }
        else if (sideSdlip[3] < -0.51 || sideSdlip[3] > 0.51 && !aiIP.handBrake)
        {
            radius = 6;
        }
        yield return new WaitForSeconds(0.3f);

        StartCoroutine(radiusUpdate());
        curveTimeCheck = true;

    }

    private void move()
    {
        if (goFoward)
        {
            if (speedBoosFlag)
            {
                rigidbody.AddForce(transform.forward * boostPower);
                Debug.Log("speedBosst wortk AI");
            }


            if (aiIP.vertical > 0)
            {
                goBack = false;
                itCanBreak = true;

                if (tType == tractionType.AW)
                {
                    for (int i = 0; i < Wheel.Length; i++)
                    {
                        Wheel[i].motorTorque = aiIP.vertical * torque / 2;

                    }
                }
                else if (tType == tractionType.RW)
                {
                    for (int i = 2; i < Wheel.Length; i++)
                    {
                        Wheel[i].motorTorque = aiIP.vertical * torque;
                    }
                }
                else if (tType == tractionType.FW)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Wheel[i].motorTorque = aiIP.vertical * torque;
                    }
                }
            }

            else if (aiIP.vertical < 0 && goBack)
            {
                goFoward = false;
                itCanBreak = true;

                if (tType == tractionType.AW)
                {
                    for (int i = 0; i < Wheel.Length; i++)
                    {
                        Wheel[i].motorTorque = aiIP.vertical * torque / 2;
                    }

                }
                else if (tType == tractionType.RW)
                {
                    for (int i = 2; i < Wheel.Length; i++)
                    {
                        Wheel[i].motorTorque = aiIP.vertical * torque;
                    }
                }
                else if (tType == tractionType.FW)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Wheel[i].motorTorque = aiIP.vertical * torque;

                    }
                }
            }
        }
        if (!goBack && itCanBreak && aiIP.vertical < 0)
        {

            if (breakBoosFlag)
            {
                rigidbody.AddForce(-transform.forward * boostPower);
                Debug.Log("breakBosst wortk AI");
            }


            for (int i = 0; i < Wheel.Length; i++)
            {
                Wheel[i].brakeTorque = aiIP.vertical * breakForce;
            }



            if (rigidbody.velocity.magnitude < 0.001 && rigidbody.velocity.magnitude > -0.001)
            {
                itCanBreak = false;
                goFoward = true;
                goBack = true;
            }
        }
        else if (!goFoward && itCanBreak && aiIP.vertical > 0)
        {
            for (int i = 0; i < Wheel.Length; i++)
            {
                Wheel[i].brakeTorque = aiIP.vertical * (-breakForce);
            }

            if (rigidbody.velocity.magnitude < 0.001 && rigidbody.velocity.magnitude > -0.001)
            {
                itCanBreak = false;
                goFoward = true;
                goBack = true;
            }
        }
        else
        {
            for (int i = 0; i < Wheel.Length; i++)
            {
                Wheel[i].brakeTorque = 0;
            }
        }
    }


    private void steerCar()
    {

        if (aiIP.horizontal > 0)
        {
            Wheel[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * aiIP.horizontal;
            Wheel[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * aiIP.horizontal;
        }
        else if (aiIP.horizontal < 0)
        {
            Wheel[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * aiIP.horizontal;
            Wheel[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * aiIP.horizontal;
        }
        else
        {
            Wheel[0].steerAngle = 0;
            Wheel[1].steerAngle = 0;
        }

    }

    private void downForce()
    {
        rigidbody.AddForce(-transform.up * rigidbody.velocity.magnitude * downForceConstant);
    }


    private void wheelAnimation()
    {

        Vector3 WheelPositon = Vector3.zero;

        Quaternion WheelRotation = Quaternion.identity;



        for (int i = 0; i < Wheel.Length; i += 2)
        {
            Wheel[i].GetWorldPose(out WheelPositon, out WheelRotation);
            meshes[i].transform.position = WheelPositon;
            meshes[i].transform.localPosition -= WPFixer;
            meshes[i].transform.rotation = WheelRotation;

        }

        for (int i = 1; i < Wheel.Length; i += 2)
        {
            Wheel[i].GetWorldPose(out WheelPositon, out WheelRotation);
            meshes[i].transform.position = WheelPositon;
            meshes[i].transform.localPosition += WPFixer;
            meshes[i].transform.rotation = WheelRotation;

        }

    }


    private void friction()
    {
        for (int i = 0; i < Wheel.Length; i++)
        {
            WheelHit wheelHit;

            Wheel[i].GetGroundHit(out wheelHit);

            fowardSdlip[i] = wheelHit.forwardSlip;
            sideSdlip[i] = wheelHit.sidewaysSlip;

        }
    }

    void getComponent()
    {
        aiIP = GetComponent<aiInput>();
        rigidbody = GetComponent<Rigidbody>();
        centerOfMass = GameObject.Find("masss");

        WPFixer.Set(-0.142742f, 0, 0);

        rigidbody.centerOfMass = centerOfMass.transform.localPosition;

        WheelColliders = GameObject.Find("WhellColliderr");

        WhellMesh = GameObject.Find("Meshess");

        meshes[0] = WhellMesh.transform.Find("0").gameObject;
        meshes[1] = WhellMesh.transform.Find("1").gameObject;
        meshes[2] = WhellMesh.transform.Find("2").gameObject;
        meshes[3] = WhellMesh.transform.Find("3").gameObject;


        Wheel[0] = WheelColliders.transform.Find("0").gameObject.GetComponent<WheelCollider>();
        Wheel[1] = WheelColliders.transform.Find("1").gameObject.GetComponent<WheelCollider>();
        Wheel[2] = WheelColliders.transform.Find("2").gameObject.GetComponent<WheelCollider>();
        Wheel[3] = WheelColliders.transform.Find("3").gameObject.GetComponent<WheelCollider>();

    }

    private void driftAndMoreGrip()
    {


        for (int i = 2; i < 4; i++)
        {
            WheelHit wheelHit;
            Wheel[i].GetGroundHit(out wheelHit);

            if (wheelHit.sidewaysSlip >= 0.3f || wheelHit.sidewaysSlip <= -0.3f || wheelHit.forwardSlip >= .3f || wheelHit.forwardSlip <= -0.3f)
            {
                smoke = true;
                reloadBoost = true;
                track = true;
                if (nitroValue < 7)
                {
                    nitroValue += Time.deltaTime / 2;
                }
            }
            else
            {
                smoke = false;
                track = false;
                reloadBoost = false;
            }


            if (wheelHit.sidewaysSlip < 0)
            {
                driftAmount = (1 - aiIP.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);
            }
            else if (wheelHit.sidewaysSlip > 0)
            {
                driftAmount = (1 + aiIP.horizontal) * Mathf.Abs(wheelHit.sidewaysSlip);
            }
        }

        if (aiIP.handBrake)
        {
            frictionFoward = Wheel[0].forwardFriction;
            frictionSide = Wheel[0].sidewaysFriction;

            // for smoothing
            frictionFoward.extremumValue = frictionFoward.asymptoteValue = frictionSide.asymptoteValue = frictionSide.extremumValue = Mathf.SmoothDamp(frictionFoward.asymptoteValue, driftAmount * handBreakFriction, ref velocity, 0.7f * Time.deltaTime);
            for (int i = 2; i < 4; i++)
            {
                Wheel[i].forwardFriction = frictionFoward;
                Wheel[i].sidewaysFriction = frictionSide;
            }

            frictionFoward.extremumValue = frictionFoward.asymptoteValue = frictionSide.asymptoteValue = frictionSide.extremumValue = 1.1f;

            for (int i = 0; i < 2; i++)
            {
                Wheel[i].forwardFriction = frictionFoward;
                Wheel[i].sidewaysFriction = frictionSide;
            }

            rigidbody.AddForce(transform.forward * (KphH * 100));

        }

        else
        {
            frictionFoward = Wheel[0].forwardFriction;
            frictionSide = Wheel[0].sidewaysFriction;

            frictionFoward.extremumValue = frictionFoward.asymptoteValue = frictionSide.asymptoteValue = frictionSide.extremumValue = (KphH * frictionnMultiplier) / 300 + 1;

            for (int i = 0; i < 4; i++)
            {
                Wheel[i].forwardFriction = frictionFoward;
                Wheel[i].sidewaysFriction = frictionSide;
            }

        }

    }
}
