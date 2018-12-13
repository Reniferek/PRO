using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsVelocity : MonoBehaviour {

    private new Rigidbody rigidbody;

    public bool lookAtOrigin;

    public Transform setPositionTo;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (rigidbody)
        {
            rigidbody.angularVelocity = Vector3.zero;
            transform.LookAt(transform.position + rigidbody.velocity);
        }

        if (setPositionTo)
        {
            transform.position = setPositionTo.position;
        }

        if (lookAtOrigin)
        {
            transform.LookAt(Vector3.zero);
        }
    }
}
