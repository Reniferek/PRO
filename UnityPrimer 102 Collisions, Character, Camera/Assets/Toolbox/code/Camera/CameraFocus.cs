using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class CameraFocus : MonoBehaviour {

    PostProcessingProfile profile;
    public Transform focusTarget;

	// Use this for initialization
	void Start ()
    {
        profile = GetComponent<PostProcessingBehaviour>().profile;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (focusTarget)
        {
            //float dist = (focusTarget.position - this.transform.position).magnitude; // doesn't handle object in corner of the view case
            float dist = Vector3.Dot(focusTarget.position - this.transform.position, this.transform.forward);
            var settings = profile.depthOfField.settings;
            settings.focusDistance = dist;
            profile.depthOfField.settings = settings;
        }
	}
}
