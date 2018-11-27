using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float period = 1;
    public float lastBallTime;

    public float rotationSpeed = 50f;
    private float direction = -1;

    public Cannonball prefab;
    public Transform cannonTransform;

	// Use this for initialization
	void Start () {
        lastBallTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        lastBallTime += Time.deltaTime;

        if (lastBallTime > period)
        {
            Instantiate(prefab, cannonTransform.transform.position, cannonTransform.rotation);
            lastBallTime = 0;
        }

        if (lastBallTime > period / 2)
        {
            float rotationValue = rotationSpeed * Time.deltaTime * direction;
            Vector3 rotateVector = new Vector3(0, 0, rotationValue);
            transform.Rotate(rotateVector);

            if (transform.rotation.z > 0 || transform.rotation.z < -0.9999)
            {
                direction *= -1;
            }
        }
	}
}
