using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    [Tooltip("Axis of rotation; will be normalized.")]
    public Vector3 axis;

    [Tooltip("Angular speed in degrees per second.")]
    public float angularSpeedInDeg;

	// Update is called once per frame
	void Update () {

        if (true)
        {
            Vector3 euler = angularSpeedInDeg * Time.deltaTime * axis.normalized;
            transform.Rotate(euler, Space.Self);
        }
        else
        {
            Quaternion rotation = Quaternion.AngleAxis(angularSpeedInDeg * Time.deltaTime, axis.normalized);
            transform.rotation = transform.rotation * rotation;
        }

    }
}
