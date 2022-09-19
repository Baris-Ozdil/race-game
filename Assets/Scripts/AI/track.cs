using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class track : MonoBehaviour
{

    public List<Transform> points = new List<Transform>();
    public Color linesColar;

    
    private void OnDrawGizmosSelected()
{
        Gizmos.color = linesColar;

        Transform[] path = GetComponentsInChildren<Transform>();

        points = new List<Transform>();

        for (int i = 1; i< path.Length; i++)
        {
            points.Add(path[i]);
        }

        for ( int i = 0; i< points.Count; i++)
        {
            Vector3 point = points[i].position;
            Vector3 prePoint = points[0].position;

            if(i != 0)
            {
                prePoint = points[i - 1].position;
            }
            else if (i == 0)
            {
                prePoint = points[points.Count - 1].position;
            }

            Gizmos.DrawLine(prePoint, point);
        }

    }


}
