using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    //Settings

    public Vector3 velocity;
    public float mass = 1f;
    public float rotation = 120.0f;

    public float gravConstantAndSunMass = 1f;

    public Transform sun;

    // Properties

    public Vector3 position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }


    // Update is called once per frame
    void Update()
    {

        position += velocity * Time.deltaTime;
        float distanceFromSun = (position - sun.position).magnitude;
        float forceMagnitude = gravConstantAndSunMass * mass / distanceFromSun * distanceFromSun;

        Vector3 force = forceMagnitude * (sun.position - position).normalized;
        Vector3 acceleration = force / mass;
        Vector3 deltaVelocity = acceleration * Time.deltaTime;

        velocity += deltaVelocity;

        this.transform.Rotate(Vector3.up, Time.deltaTime * rotation);
    }

    void OnDrawGizmos()
    {

    }
}
