// © 2018 adrian.licensing@gmail.com This Software is made available under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox
{
	public class Rotator : MonoBehaviour
	{
        // - - - Settings - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public Vector3 angularVelocityDegPerSec;

        public bool useHelperFunction;

		void Update() 
		{
            if (useHelperFunction)
            {
                transform.RotateAround(Vector3.zero, angularVelocityDegPerSec, angularVelocityDegPerSec.magnitude * Time.deltaTime);
                transform.LookAt(Vector3.zero);
            }
            else
            {
                Quaternion deltaRotation = Quaternion.AngleAxis(angularVelocityDegPerSec.magnitude * Time.deltaTime, angularVelocityDegPerSec);

                transform.position = deltaRotation * transform.position;
                transform.rotation = deltaRotation * transform.rotation;
            }
		}
	}
}
