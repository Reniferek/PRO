// © 2018 adrian.licensing@gmail.com This Software is made available under the MIT License.

using UnityEngine;

namespace Toolbox
{

    // TODO: remember reference to last supported moving object, to move with it
    // TODO: Fix x,z when no velocity given (to avoid slipping off surfaces), and when isSupported

    [RequireComponent(typeof(Rigidbody))]
	public class PhysicalCharacterController : MonoBehaviour
	{
        // - - - Settings - - - - - - - - - - - - - - - - - - - - - - -

        public float speed = 6f;
        public bool rotateTowardsVelocity = true;
        public float angularSpeed = 720f;

        public float jumpHeight = 2f;
        [Tooltip("Not implemented")]
        public bool allowDoubleJump = true;
        public float gravityScale = 2f;
        public float maxSupportAngle = 60f;

        public Vector3 right = Vector3.right;
        public Vector3 forward = Vector3.forward;

		// - - - Runtime state - - - - - - - - - - - - - - - - - - - - -
        
        private int jumpCountSinceLastSupported;

        private new Rigidbody rigidbody;

        // - - - Public methods - - - - - - - - - - - - - - - - - - - -

        // - - - Unity events - - - - - - - - - - - - - - - - - - - - -

        private void OnCollisionStay(Collision collision)
        {
            hasContactsWithUpwardsNormal = hasContactsWithUpwardsNormal || HasSupportingContacts(collision.contacts);
        }

        private void OnValidate()
        {
            minSupportCosAngle = Mathf.Cos(maxSupportAngle);
            Debug.Log("Validated");
        }

        // - - - Unity update loop - - - - - - - - - - - - - - - - - - -

        void Start() 
		{
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.freezeRotation = true;
            rigidbody.inertiaTensor = float.MaxValue * Vector3.one;
        }

        void Update() 
		{
            float horizontal = Input.GetAxis(horizontalAxis);
            float vertical = Input.GetAxis(verticalAxis);
            Vector3 velocity = horizontal * right + vertical * forward;
            velocity *= speed;

            //transform.position += velocity * Time.deltaTime;

            if (isSupported)
            {
                jumpCountSinceLastSupported = 0;
            }

            bool clearVerticalSpeedToo = false;
            if (Input.GetButtonDown(jumpAxis) && (isSupported || jumpCountSinceLastSupported < (allowDoubleJump ? 1 : 0)))
            {
                // s = a*t*t/2 => t = Mathf.Sqrt(s/a*2)
                // v = a * t
                float g = Physics.gravity.magnitude * gravityScale;
                float time = Mathf.Sqrt(2f * jumpHeight / g);
                float vel = time * g;

                //transform.position += speed * Input.GetAxis(jumpAxis) * Vector3.up;// * Time.deltaTime;
                velocity += vel * Vector3.up;
                clearVerticalSpeedToo = true;

                jumpCountSinceLastSupported++;
            }

            if (clearVerticalSpeedToo)
            {
                rigidbody.velocity = Vector3.zero;
            }

            rigidbody.velocity = velocity + Vector3.up * Vector3.Dot(Vector3.up, rigidbody.velocity);

            if (rotateTowardsVelocity )
            {
                Vector3 direction = velocity;
                direction.y = 0f;
                if (direction.sqrMagnitude > 0.01f * 0.01f)
                {
                    //float sinAngle = Vector3.Cross(Vector3.forward, velocity.normalized).magnitude;
                    //float angle = Mathf.Asin(sinAngle) * Mathf.Rad2Deg;
                    float angle = Vector3.SignedAngle(Vector3.forward, velocity, Vector3.up);
                    rotation = Quaternion.RotateTowards(rotation, Quaternion.AngleAxis(angle, Vector3.up), angularSpeed * Time.deltaTime);
                }
            }
        }

        private void FixedUpdate()
        {
            if (rigidbody.useGravity)
            {
                rigidbody.AddForce(Physics.gravity * (gravityScale - 1f), ForceMode.Acceleration);
            }

            isSupported = hasContactsWithUpwardsNormal;
            hasContactsWithUpwardsNormal = false;
        }

        // - - - Unity construction & destruction - - - - - - - - - - - -

        void Awake()
		{
		}

		void OnDestroy()
		{
		}

		void OnEnable()
		{
		}

		void OnDisable()
		{
		}

		// - - - Private and protected methods - - - - - - - - - - - -

        protected Vector3 position { get { return transform.position; } set { transform.position = value; } }
        protected Quaternion rotation { get { return transform.rotation; } set { transform.rotation = value; } }

        ///<summary>Is character supported by upwards facing contact points.</summary>
        public bool isSupported { get; private set; }
        private bool hasContactsWithUpwardsNormal;

        private float minSupportCosAngle { get; set; }

        ///<summary>Must execute after hot reload.</summary>
        void InitReferences()
        {
        }

        bool HasSupportingContacts(ContactPoint[] contacts)
        {
            foreach (ContactPoint contact in contacts)
            {
                if (contact.separation < 0.01f && minSupportCosAngle <= Vector3.Dot(contact.normal, Vector3.up))
                {
                    return true;
                }
            }
            return false;
        }

        // - - - Debug methods - - - - - - - - - - - - - - - - - - - -

        void OnDrawGizmos()
		{
		}

        // - - - Static & constants - - - 

        public static string horizontalAxis = "Horizontal";
        public static string verticalAxis = "Vertical";
        public static string jumpAxis = "Jump";

        private int reloadCount = 0; 

        [UnityEditor.Callbacks.DidReloadScripts]
        static void OnHotReloadFinished()
        {
            //var controller = GameObject.FindObjectOfType<PhysicalCharacterController>();
            //Debug.Log("PLAYER SCRIPT RELOADED (" + controller.reloadCount++ + ")");
        }
    }
}
