// © 2018 adrian.licensing@gmail.com This Software is made available under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox
{
	public class DriveControllerOrientationFromTransform : MonoBehaviour
	{
        // - - - Settings - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public Transform source;
        public PhysicalCharacterController controller;

		// - - - Runtime state - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		// - - - Public methods - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		// - - - Unity events - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		// - - - Unity update loop - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		void Start() 
		{
			
		}
	
        Vector3 ProjectOnPlane(Vector3 v, Vector3 normal)
        {
            return v - normal * Vector3.Dot(normal, v);
        }

		void Update() 
		{
            controller.forward = ProjectOnPlane(source.forward, Vector3.up).normalized;
            controller.right = ProjectOnPlane(source.right, Vector3.up).normalized;
        }

		// - - - Unity construction & destruction - - - - - - - - - - - - - - - - - - - - - - - - -

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

		// - - - Private and protected methods - - - - - - - - - - - - - - - - - - - - - - - - - - -

		// - - - Editor methods - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		void OnValidate()
		{
			
		}

		// - - - Debug methods - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		void OnDrawGizmos()
		{
			
		}

		// - - - Static section - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	}
}
