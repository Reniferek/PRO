// © 2018 adrian.licensing@gmail.com This Software is made available under the MIT License.

using UnityEngine;
using UnityEngine.UI;

namespace Toolbox
{
	public class SystemEnergy : MonoBehaviour
	{
        // - - - Settings - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public Text console;

        // - - - Runtime state - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        private BounceInABox[] bouncingObjects;

		// - - - Unity update loop - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

		void Start() 
		{
            bouncingObjects = GameObject.FindObjectsOfType<BounceInABox>();
		}
	
		void Update() 
		{
            if (console)
            {
                float totalEnergy = 0f;
                foreach (var b in bouncingObjects)
                {
                    totalEnergy += b.energy;
                }

                console.text = "Total System Energy = " + totalEnergy;
            }
        }
	}
}
