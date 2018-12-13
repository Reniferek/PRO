// © 2018 adrian.licensing@gmail.com This Software is made available under the MIT License.

using UnityEngine;

namespace Toolbox
{
	public class BounceInABox : MonoBehaviour
	{
        // Settings

        public Vector3 startVelocity = Vector3.forward;

        public float mass = 1f;

        public Bounds bounds = new Bounds(Vector3.zero, 10f * Vector3.one);

        [System.NonSerialized]
        public Vector3 gravity = 9.81f * Vector3.down;

        public float restitution = 1f;

        // Runtime state
        private Vector3 velocity;
        private Vector3 position;

        // Public interface

        public float energy
        {
            get
            {
                float height = Vector3.Dot(position - bounds.center, -gravity.normalized);
                float potentialEnergy = mass * gravity.magnitude * height;
                float kineticEnergy = 0.5f * mass * Vector3.Dot(velocity, velocity);
                float energy = potentialEnergy + kineticEnergy;
                return energy;
            }
        }

        // Unity methods


        void Start()
        {
            velocity = startVelocity;
            position = transform.position;
        }

        private void Update()
        {
            transform.position = position;
        }

        void FixedUpdate()
        {
            Vector3 deltaVelocity = gravity * Time.deltaTime;
            velocity += deltaVelocity;

            position += velocity * Time.deltaTime;

            for(int i = 0; i < 3; i++)
            {
                if (position[i] < bounds.min[i])
                {
                    velocity[i] *= -restitution;
                }
                if (position[i] > bounds.max[i])
                {
                    velocity[i] *= -restitution;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }


    }
}
