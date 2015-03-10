using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour {

    public Transform[] points;

    void OnDrawGizmos()
    {
        iTween.DrawPath(points);
    }
}
