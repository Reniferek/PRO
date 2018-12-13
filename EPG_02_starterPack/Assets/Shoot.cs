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
    public Transform player;

	// Use this for initialization
	void Start () {
        lastBallTime = 0;
        GameObject.Find("Cannonball").transform.position = new Vector3(100, 100, 0);
        //player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        lastBallTime += Time.deltaTime;
        Vector3 targetDir = player.position - transform.position;
        Vector3 right = -transform.right;
        float angle = Vector3.Angle(targetDir, right);
        if (Vector3.Distance(player.transform.position, transform.position) < 5 && lastBallTime > period && angle < 15f)
        {
            transform.right = -(player.position - transform.position);
            Instantiate(prefab, cannonTransform.transform.position, cannonTransform.rotation);
            lastBallTime = 0;
        }
        else
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
