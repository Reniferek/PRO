// © 2018 adrian.licensing@gmail.com This Software is made available under the MIT License.

using UnityEngine;

namespace Toolbox
{
    public class SimpleFollowCamera : MonoBehaviour
	{
        // - - - Settings - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public Transform target;

        [Tooltip("In fixedOffset mode, modify camera's transform directly")]
        public float followDistance;

        [Header("Control mode")]
        [Tooltip("This control mode also influences behavior of the camera's Transform pos & rotation inspector.")]
        public bool freezeRotation;

        [Tooltip("Keeps camera at constant height, and fixed rotation pitch. Good for freezing camera motion during jumping")]
        public bool freezeHeightAndPitch;

        // - - - Runtime state - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        private Vector3 offsetFromTarget;

        // - - - Public methods - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        // - - - Unity events - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        // - - - Unity update loop - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        void Start() 
		{
            if (target)
            {
                if (followDistance <= 0f)
                {
                    offsetFromTarget = transform.position - target.position;
                    InitFollowDistance(offsetFromTarget);
                }
                else
                {
                    offsetFromTarget = CalcOffsetFromDistance();
                }
            }
        }

        void Update() 
		{
            if (target)
            {
                if (freezeRotation)
                {                
                    if (freezeHeightAndPitch)
                    {
                        Vector3 newPosition = target.position + offsetFromTarget;
                        newPosition.y = transform.position.y;
                        transform.position = newPosition;
                    }
                    else
                    {
                        transform.position = target.position + offsetFromTarget;
                    }
                }
                else
                {
                    Vector3 delta = target.position - transform.position;
                    Vector3 fullDelta = delta;
                    if (freezeHeightAndPitch)
                    {
                        delta.y = 0f;
                    }
                    float deltaMagnitude = delta.magnitude;
                    if (deltaMagnitude > 1e-6)
                    {
                        transform.position += (deltaMagnitude - followDistance) * delta / deltaMagnitude;

                        Vector3 eulerToTarget = Quaternion.LookRotation(delta).eulerAngles;
                        if (freezeHeightAndPitch)
                        {
                            eulerToTarget.x = transform.rotation.eulerAngles.x;
                        }
                        transform.rotation = Quaternion.Euler(eulerToTarget);
                    }
                }
            }
        }

		// - - - Unity construction & destruction - - - - - - - - - - - - - - - - - - - - - - - - -

		void Awake() {  } 
		void OnDestroy() {  } 
		void OnEnable() {  } 
		void OnDisable() {  }

        // - - - Private and protected methods - - - - - - - - - - - - - - - - - - - - - - - - - - -


        // - - - Editor methods - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        void InitFollowDistance(Vector3 offset)
        {
            if (freezeHeightAndPitch)
            {
                offset.y = 0f;
            }
            followDistance = offset.magnitude;
        }

        Vector3 CalcOffsetFromDistance()
        {
            Vector3 result;
            if(!freezeHeightAndPitch)
            {
                result = (transform.position - target.position).normalized * followDistance;
            }
            else
            {
                Vector3 targetProjection = target.position;
                targetProjection.y = transform.position.y;
                Vector3 desiredCameraTransform = targetProjection + (transform.position - targetProjection).normalized * followDistance;
                result = desiredCameraTransform - target.position;
            }

            return result;
        }


        // - - - Debug methods - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        void OnDrawGizmos()
        {

        }

		// - - - Static section - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	}
}
