using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestruction : MonoBehaviour {

    public float timeToLive = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeToLive -= Time.deltaTime;
        if (timeToLive < 0f)
        {
            Destroy(gameObject);
        }
	}
}
