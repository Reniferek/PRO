using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class StartVelocity : MonoBehaviour
    {
        public Vector3 direction;
        public float speed;

        // Use this for initialization
        void Start()
        {
            rigidbody.velocity = speed * direction.normalized;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public new Rigidbody rigidbody { get { return GetComponent<Rigidbody>(); } }
    }
}
