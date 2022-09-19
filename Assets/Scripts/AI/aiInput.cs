using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiInput : MonoBehaviour
{
    public track point;
    public List<Transform> points = new List<Transform>();
    public Transform currentPoint;
    public float vertical;
    public float horizontal;
    public bool handBrake;
    public bool boost;
    private int distanceOffset = 2;
    private float Currentdistance;
    public aiControler aiCont;


    // Start is called before the first frame update
    private void Awake()
    {
        aiCont = gameObject.GetComponent<aiControler>();
        point = GameObject.FindGameObjectWithTag("trackPoints").GetComponent<track>();

        points = point.points;
    }

    //Update is called once per frame
    private void FixedUpdate()
    {
        distanceCalculater();
        AI();
    }

    private void AI()
    {

        vertical = 0.5f;
        if (aiCont.Kph > 120 && (currentPoint == points[7] || currentPoint == points[8] || currentPoint == points[9] || currentPoint == points[19]))
        {
            vertical = -1f;
        }
        else if (aiCont.Kph > 100 && (currentPoint == points[20] || currentPoint == points[21] || currentPoint == points[22] || currentPoint == points[23] /*|| currentPoint == points[36] || currentPoint == points[37]*/))
        {
            vertical = -0.8f;
        }
        else if (aiCont.Kph > 80 && (currentPoint == points[36] || currentPoint == points[37]))
        {
            vertical = -0.8f;
        }
        else if (aiCont.Kph > 60 && (currentPoint == points[38] || currentPoint == points[39]))
        {
            vertical = -0.8f;
        }
        else if (aiCont.Kph > 100 && (currentPoint == points[34] || currentPoint == points[35]))
        {
            vertical = -1f;
        }
        else if (aiCont.Kph > 160 && (currentPoint == points[6] ))
        {
            vertical = -1f;
        }
        else if (aiCont.Kph > 150 && (currentPoint == points[18]))
        {
            vertical = -1f;
        }
        else if (aiCont.Kph > 180 && (currentPoint == points[5]))
        {
            vertical = -1f;
        }
        else if (aiCont.Kph > 200 && (currentPoint == points[4]))
        {
            vertical = -1f;
        }
        else 
        {
            vertical = 1f;
        }
            steer();

    }

    private void steer()
    {
        Vector3 target = transform.InverseTransformPoint(currentPoint.transform.position);
        target /= target.magnitude;

        horizontal = (target.x / target.magnitude) * 3;
    }

    private void distanceCalculater()
    {
        Vector3 positon = gameObject.transform.position;
        float distance = Mathf.Infinity;

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 difrence = points[i].transform.position - positon;
            Currentdistance = difrence.magnitude;
            if (Currentdistance < distance)
            {
                currentPoint = points[i + distanceOffset];
                distance = Currentdistance;

                if ((i + distanceOffset) >= points.Count)
                {
                    currentPoint = points[1];
                    distance = Currentdistance;
                }
                else
                {
                    currentPoint = points[i + distanceOffset];
                    distance = Currentdistance;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(currentPoint.position, 3);
    }
}
