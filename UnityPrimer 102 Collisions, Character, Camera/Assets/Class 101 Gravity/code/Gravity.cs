using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Textures from https://www.solarsystemscope.com/textures/

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour {

    public Vector3 startVelocity = Vector3.forward;

    public Transform sun;
    public float sunMass = 1f;
    public float gravitationalConstant = 1f;

    public bool useCustomPhysics = true;
    public int numSubsteps = 100;
    public float radiusExponent = 2f;

    public Transform projectionPlane;
    public float projectionLineAlpha = 0.1f;
    public float distanceLineAlpha = 0.025f;

    private new Rigidbody rigidbody;
    private Vector3 velocity;
    private Vector3 position;


	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = startVelocity;
        velocity = startVelocity;
        position = transform.position;
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (sun)
        {
            // Do calcs either way
            for (int i = 0; i < numSubsteps; i++)
            {
                float deltaTime = Time.deltaTime / numSubsteps;

                Vector3 dist = sun.position - position;
                //float force = gravitationalConstant * sunMass * rigidbody.mass / dist.sqrMagnitude;
                float force = gravitationalConstant * sunMass * rigidbody.mass / Mathf.Pow(dist.magnitude, radiusExponent);
                Vector3 forceV = force * dist.normalized;
                Vector3 impulse = forceV * deltaTime;
                velocity += impulse / rigidbody.mass;
                position += velocity * deltaTime;
            }


            if (useCustomPhysics)
            {
                transform.position = position;
                rigidbody.velocity = velocity;// Vector3.zero;
            }
            else
            {
                Vector3 dist = sun.position - this.transform.position;

                // Compute gravity: F = G M m / r^2
                float force = gravitationalConstant * sunMass * rigidbody.mass / dist.sqrMagnitude;
                rigidbody.AddForce(force * dist.normalized);
            }

        }
    }

    private void OnDrawGizmos()
    {
        if (!useCustomPhysics)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(position, 0.5f * transform.lossyScale.x);
        }

        // Draw projection onto plane
        if (projectionPlane)
        {
            Vector3 planeOriginToPosition = this.transform.position - projectionPlane.position;
            Vector3 planeNormal = projectionPlane.up;
            float displacementDotNormal = Vector3.Dot(planeOriginToPosition, planeNormal);
            Vector3 projected = transform.position - displacementDotNormal * planeNormal;

            Color gizmoColor = Color.green;
            gizmoColor.a = projectionLineAlpha;
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(projected, 0.05f);
            Gizmos.DrawLine(projected, transform.position);

            gizmoColor.a = distanceLineAlpha;
            Gizmos.color = gizmoColor;
            Gizmos.DrawLine(projected, Vector3.zero);
        }
    }
}
